
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrderService.BusinessLogicLayer.Validators;

public class OrderAddRequestValidator:AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        //UserID
        RuleFor(temp => temp.UserID)
            .NotEmpty().WithErrorCode("User ID cannot be empty");

        //Order Date
        RuleFor(temp => temp.OrderDate)
            .NotEmpty().WithErrorCode("User Date cannot be empty");

        //Order Item
        RuleFor(temp => temp.OrderItems)
            .NotEmpty().WithErrorCode("Order item cannot be empty");
    }
}
