using FluentValidation;
using OrderService.Models;

namespace OrderService.Validators;

public class OrderRequestValidator : AbstractValidator<SpecifiedDeliveryOrder>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Required").GreaterThan(0).WithMessage("Invalid product id value.");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Required");
        RuleFor(x => x.DeliveryDay).NotEmpty().WithMessage("Required").GreaterThan(x => DateTime.Today.AddDays(1)).WithMessage("Delivery Day Can Not Be The Same Day When Order Is Made.");
        RuleFor(x => x.DeliveryDay).LessThan(x => DateTime.Today.AddDays(14)).WithMessage("Can Not Place Order That Should Be Delivered After 14 Days.");
    }
}
