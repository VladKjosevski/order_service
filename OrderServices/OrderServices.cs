using OrderService.Data;
using OrderService.Enums;
using OrderService.Exceptions;
using OrderService.Extentions;
using OrderService.Models;
using OrderService.Validators;

namespace OrderService.OrderService;

public interface IOrderServices
{
    Task<List<AvailableDeliveries>> AvalilableDelivery(List<SpecifiedDeliveryOrder> orderItems);
    Task<List<AvailableDeliveries>> AvalilableDeliveryDates(List<OrderRequest> orderItems);
}
public class OrderServices : IOrderServices
{
    private readonly IProductItems _productItems;
    private readonly ILogger<OrderServices> _logger;
    public OrderServices(IProductItems productItems, ILogger<OrderServices> logger)
    {
        _productItems = productItems;
        _logger = logger;
    }

    public async Task<List<AvailableDeliveries>> AvalilableDelivery(List<SpecifiedDeliveryOrder> orderItems)
    {        
        var validOrders = ValidateOrders.ValidOrders(orderItems);

        if (!validOrders.Any())
        {
            _logger.LogInformation("Orders Can Not Be Accepted");
           return new List<AvailableDeliveries>();
        } 
                
        var productId = validOrders.Select(x => x.ProductId).ToList();

        var products = await _productItems.GetProductById(productId);
        
        var confirmedOrders = products.Select(x => x.Deliveries(orderItems)).ToList();

        return DeliveringBy(confirmedOrders);
    }

    public async Task<List<AvailableDeliveries>> AvalilableDeliveryDates(List<OrderRequest> orderItems)
    {     
        var availableDaysToDeliver = new List<AvailableDeliveries>();   
        var productId = orderItems.Select(x => x.ProductId).ToList();
        var products = await _productItems.GetProductById(productId);


        foreach (var orderItem in orderItems)
        {           
            var productType = EnumValueType.GetProductType(orderItem.ProductId);       
            var deliveryDays = CanBeDeliveredOnDays(productType.ToString());
            var deliveryDates = CalculateDeliveryDates(productType.ToString());
            var available = CalculateDates(deliveryDays, deliveryDates);
            var deliveryDetails = orderItem.AvailableDatesForDelivery(available,products);
            availableDaysToDeliver.AddRange(deliveryDetails);
        }
        
        return DeliveringBy(availableDaysToDeliver);        
    }

    private static List<string> CanBeDeliveredOnDays(string productType)
    {        
        return productType == ProductType.External.ToString()? 
            DeliveryDays.ExternalProducts : productType == ProductType.Normal.ToString()?
            DeliveryDays.NormalProducts : DeliveryDays.TemporaryProducts;
    }

    private static List<DateTime> CalculateDeliveryDates(string productType)
    {
        var days = OrderInAdvance(productType);

        DateTime earliestDeliveryDate = DateTime.Now.AddDays(days);   
        
        var dayToday = (int)DateTime.Today.DayOfWeek;

        var lastDayOfWeek = DateTime.Now.AddDays(7 - dayToday);

        DateTime latestDeliveryDate = DateTime.Now.AddDays(14);

        var availableDays = new List<DateTime>();

        if(productType == ProductType.Temporary.ToString())
        {
            while (DateTime.Compare(earliestDeliveryDate, lastDayOfWeek) == -1)
            {
                availableDays.Add(earliestDeliveryDate);
                earliestDeliveryDate = earliestDeliveryDate.AddDays(1);
            }
        }
        else
        {           
            while ((DateTime.Compare(earliestDeliveryDate, latestDeliveryDate)) == -1)
            {
                availableDays.Add(earliestDeliveryDate);
                earliestDeliveryDate = earliestDeliveryDate.AddDays(1);               
            }
        }
        return availableDays;
    }   

    private static int OrderInAdvance(string productType)
    {      
         return  productType == ProductType.External.ToString() ?
            DaysInAdvanceToOrder.ExternalProducts : productType == ProductType.Normal.ToString() ?
            DaysInAdvanceToOrder.NormalProducts : DaysInAdvanceToOrder.TemporaryProducts;
    }

    private static List<DateTime> CalculateDates(List<string>days, List<DateTime>dates)
    {
        var availableDeliveries = new List<DateTime>();
        Dictionary<string, List<DateTime>> datesDict = new Dictionary<string, List<DateTime>>();
        foreach (var date in dates)
        {
            if (!datesDict.ContainsKey(date.DayOfWeek.ToString()))
            {
                datesDict.Add(date.DayOfWeek.ToString(), new List<DateTime> { date });
            }
            else
            {
                datesDict[date.DayOfWeek.ToString()].Add(date);
            }
        }
        foreach(var day in days)
        {
            if (datesDict.ContainsKey(day))
            {
                availableDeliveries.AddRange(datesDict[day]);
            }
        }
        return availableDeliveries;        
    }

    private static List<AvailableDeliveries> DeliveringBy (List<AvailableDeliveries> deliveryList)
    {
        bool greenDelivery = false;
        var dayReference = DateTime.Now.AddDays(3);

        foreach(var delivery in deliveryList)
        {
            if(delivery.IsGreenDelivery==true && delivery.DeliveryDate <= dayReference)
            {
                greenDelivery = true;
            }
        }
        return greenDelivery == true ? deliveryList.OrderByDescending(x => x.IsGreenDelivery).ThenBy(x =>x.DeliveryDate).ToList() 
            : deliveryList;
    }

}
