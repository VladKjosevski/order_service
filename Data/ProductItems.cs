using OrderService.Enums;
using OrderService.Exceptions;
using OrderService.Models;

namespace OrderService.Data;

public interface IProductItems
{
    Task<List<Product>> GetProductById(List<int> ids);
}
public class ProductItems : IProductItems
{
    public async Task<List<Product>> GetProductById(List<int> ids)
    {
        Dictionary<int, Product> listOfProducts = new Dictionary<int, Product>();

        listOfProducts.Add(1, new Product() { ProductID = 1, Name = "N1", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(2, new Product() { ProductID = 2, Name = "N2", DeliveryDays = DeliveryDays.ExternalProducts, ProductType = ProductType.External, DaysInAdvance = DaysInAdvanceToOrder.ExternalProducts });
        listOfProducts.Add(3, new Product() { ProductID = 3, Name = "N3", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(4, new Product() { ProductID = 4, Name = "N4", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(5, new Product() { ProductID = 5, Name = "N5", DeliveryDays = DeliveryDays.TemporaryProducts, ProductType = ProductType.Temporary, DaysInAdvance = DaysInAdvanceToOrder.TemporaryProducts });
        listOfProducts.Add(6, new Product() { ProductID = 6, Name = "N6", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(7, new Product() { ProductID = 7, Name = "N7", DeliveryDays = DeliveryDays.ExternalProducts, ProductType = ProductType.External, DaysInAdvance = DaysInAdvanceToOrder.ExternalProducts });
        listOfProducts.Add(8, new Product() { ProductID = 8, Name = "N8", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(9, new Product() { ProductID = 9, Name = "N9", DeliveryDays = DeliveryDays.NormalProducts, ProductType = ProductType.Normal, DaysInAdvance = DaysInAdvanceToOrder.NormalProducts });
        listOfProducts.Add(10, new Product() { ProductID = 10, Name = "N10", DeliveryDays = DeliveryDays.TemporaryProducts, ProductType = ProductType.Temporary, DaysInAdvance = DaysInAdvanceToOrder.TemporaryProducts });
        List<Product> products = new List<Product>();

        foreach (var id in ids)
        {
            listOfProducts.TryGetValue(id, out Product item);
            if (item != null) products.Add(item);
        }
        if (products.Count == 0) throw new IncorrectOrderException("One or More Products Can Not Be Found");
        return products;        
                
    }
}
