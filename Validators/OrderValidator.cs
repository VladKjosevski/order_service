using FluentValidation;
using OrderService.Models;

namespace OrderService.Validators;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    public OrderValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Required").GreaterThan(0).WithMessage("Invalid product id value");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Required");
       
    }
}