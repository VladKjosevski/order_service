namespace OrderService.Models;

public class SpecifiedDeliveryOrder : OrderRequest
{
    public DateTime DeliveryDay { get; set; }
    
}
