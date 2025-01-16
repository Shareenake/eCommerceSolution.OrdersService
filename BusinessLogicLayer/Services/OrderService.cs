

using AutoMapper;
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrderService.DataAccessLayer.RepositoryContracts;
using FluentValidation;

namespace eCommerce.OrderService.BusinessLogicLayer.Services;

public class OrderService:IOrderService
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
    private readonly IValidator<OrderUpdateRequest> orderUpdateRequestValidator;
    private readonly IValidator<OrderItemAddRequest> orderItemAddRequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidato;
    public OrderService(IOrderRepository orderRepository,IMapper mapper,
        IValidator<OrderAddRequest> orderAddRequestValidator,
        IValidator<OrderUpdateRequest> orderUpdateRequestValidator,
        IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
        IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator)
    {

        _orderRepository = orderRepository;

    }
}
