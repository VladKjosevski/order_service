using OrderService.Models;

namespace OrderService.Extentions;

public static class ProductsToDelivery
{
    public static AvailableDeliveries Deliveries(this Product product, List<SpecifiedDeliveryOrder> orderItems)
    {
        var orders = orderItems.ToDictionary(x => x.ProductId);

        var item = orders[product.ProductID];
        return new AvailableDeliveries
        {
            ProductId = item.ProductId,
            PostalCode = item.PostalCode,
            DeliveryDate = item.DeliveryDay,
            IsGreenDelivery = IsGreenDelivery(item.DeliveryDay)
        };
    }

    public static List<AvailableDeliveries> AvailableDatesForDelivery(this OrderRequest order,List<DateTime> dates, List<Product>products)
    {
        var deliveries = new List<AvailableDeliveries>();
        foreach (var date in dates)
        {
            var delivery = new AvailableDeliveries
            {
                ProductId = order.ProductId,
                PostalCode = order.PostalCode,
                DeliveryDate = date,
                IsGreenDelivery = IsGreenDelivery(date)
            };
            deliveries.Add(delivery);
        }       

        return deliveries;
    }
    private static bool IsGreenDelivery(DateTime deliveryDate)
    {
        int greenDay = 5;
        int delivery = deliveryDate.Day;
        return ((delivery % greenDay) == 0) ? true : false;
    }
}
