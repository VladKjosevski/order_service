using OrderService.Enums;

namespace OrderService.Models;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public List<string> DeliveryDays { get; set; }
    public ProductType ProductType { get; set; }
    public int DaysInAdvance { get; set; }
}
