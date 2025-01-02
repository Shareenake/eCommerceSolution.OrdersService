
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrderService.BusinessLogicLayer.Validators;

public class OrderItemUpdateRequestValidator : AbstractValidator<OrderItemUpdateRequest>
{
    public OrderItemUpdateRequestValidator()
    {
        //ProductID
        RuleFor(temp => temp.ProductID)
            .NotEmpty().WithErrorCode("Product ID cannot be empty");

        //Order Date
        RuleFor(temp => temp.UnitPrice)
            .NotEmpty().WithErrorCode("Unit price cannot be empty")
            .GreaterThan(0).WithErrorCode("Unit price cannot be less than or equal to zero");

        //Order Item
        RuleFor(temp => temp.Quantity)
            .NotEmpty().WithErrorCode("Quantity cannot be empty")
            .GreaterThan(0).WithErrorCode("Quantity cannot be less than or equal to zero");
    }
}
