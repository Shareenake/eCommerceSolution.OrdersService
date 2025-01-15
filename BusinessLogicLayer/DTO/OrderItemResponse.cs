

namespace eCommerce.OrderService.BusinessLogicLayer.DTO
{
    public record OrderItemResponse(Guid ProductID,decimal TotalPrice, decimal UnitPrice, int Quantity)
    {
        public OrderItemResponse() : this(default,default, default, default)
        {

        }
    }
}
