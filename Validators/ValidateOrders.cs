using OrderService.Data;
using OrderService.Enums;
using OrderService.Models;

namespace OrderService.Validators;

public static class ValidateOrders
{
    public static List<SpecifiedDeliveryOrder> ValidOrders(List<SpecifiedDeliveryOrder> orderItems)
    {
        var validOrders = new List<SpecifiedDeliveryOrder>();
                
        foreach (var order in orderItems)
        {
            //get product type
            var productType = EnumValueType.GetProductType(order.ProductId);

            //get day of week
            var dayToDeliver = order.DeliveryDay.DayOfWeek.ToString();

            var result = order.CanBeDeliveredOnDay(dayToDeliver, productType.ToString());
            if(result.ProductId !=0)
            {
                validOrders.Add(order);
            }           
        }
        return validOrders;
    }

    private static SpecifiedDeliveryOrder CanBeDeliveredOnDay(this SpecifiedDeliveryOrder order, string dayToDelivery, string productType)
    {
        if (productType == ProductType.External.ToString())
        {
           return DeliveryDays.ExternalProducts.Contains(dayToDelivery) ? OnTimeOrdered(order,DaysInAdvanceToOrder.ExternalProducts) : new SpecifiedDeliveryOrder();
        }

        if (productType == ProductType.Normal.ToString())
        {
            return DeliveryDays.NormalProducts.Contains(dayToDelivery) ? OnTimeOrdered(order,DaysInAdvanceToOrder.NormalProducts) : new SpecifiedDeliveryOrder();
        }

        if (productType == ProductType.Temporary.ToString())
        {
            return DeliveryDays.TemporaryProducts.Contains(dayToDelivery) ? IsValidTemporaryOrder(order) : new SpecifiedDeliveryOrder();
        }
        return new SpecifiedDeliveryOrder();
    }

    private static SpecifiedDeliveryOrder IsValidTemporaryOrder(SpecifiedDeliveryOrder order)
    {        
        var dayToday =(int)DateTime.Today.DayOfWeek;
        var lastDayOfWeek = DateTime.Now.AddDays(7 - dayToday);
        
        return order.DeliveryDay <= lastDayOfWeek ? order : new SpecifiedDeliveryOrder();        
    }

    private static SpecifiedDeliveryOrder OnTimeOrdered(SpecifiedDeliveryOrder order, int day)
    {
        var minDateToDeliver = DateTime.Now.AddDays(day);
        return order.DeliveryDay >= minDateToDeliver ? order : new SpecifiedDeliveryOrder();
    }
}
