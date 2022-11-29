using OrderService.Exceptions;
using System.Runtime.CompilerServices;

namespace OrderService.Enums;

public enum ProductType
{
    Normal,
    External,
    Temporary
}


public static class EnumValueType
{
    public static ProductType GetProductType(this int id)
    {
        switch (id)
        {
            case 1:
            case 3:
            case 4:
            case 6:
            case 8:
            case 9:
            return ProductType.Normal;                
            case 2:
            case 7:
             return ProductType.External;
            case 5:
            case 10:
             return  ProductType.Temporary;
            default:
                throw new IncorrectOrderException("One or More Products Can Not Be Found");
        }
    }
}




