

using AutoMapper;
using eCommerce.OrderService.BusinessLogicLayer.Client;
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrderService.BusinessLogicLayer.Validators;
using eCommerce.OrderService.DataAccessLayer.Entities;
using eCommerce.OrderService.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Driver;

namespace eCommerce.OrderService.BusinessLogicLayer.Services;

public class OrdersService:IOrderService
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
    private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator;
    private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator;
    private readonly UserMicroServiceClient _userMicroServiceClient;
    public OrdersService(IOrderRepository orderRepository,
        IMapper mapper,
        IValidator<OrderAddRequest> orderAddRequestValidator,
        IValidator<OrderUpdateRequest> orderUpdateRequestValidator,
        IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
        IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator,
        UserMicroServiceClient userMicroServiceClient
        )
    {
        _orderAddRequestValidator = orderAddRequestValidator;
        _orderUpdateRequestValidator = orderUpdateRequestValidator;
        _orderItemAddRequestValidator = orderItemAddRequestValidator;
        _orderItemUpdateRequestValidator = orderItemUpdateRequestValidator;
        _orderRepository = orderRepository;
        _userMicroServiceClient = userMicroServiceClient;
        _mapper = mapper;

    }

    public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
    {
        //Check if Order Add Request is null or not
        if(orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }

        //Validate OrderRequest using Fluent Validation
        ValidationResult orderAddRequestValidationResult=await _orderAddRequestValidator.ValidateAsync(orderAddRequest);
        if (!orderAddRequestValidationResult.IsValid)
        {
            string errors = string.Join(",", orderAddRequestValidationResult.Errors.Select
                (temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
           
        }

        //Validate OrderItems using Fluent validation
        foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItems)
        {
            ValidationResult orderItemAddRequestValidationResult=await _orderItemAddRequestValidator.ValidateAsync(orderItemAddRequest);
            if(!orderItemAddRequestValidationResult.IsValid)
            {
                string errors=string.Join(",",orderItemAddRequestValidationResult.Errors.Select
                    (temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }

        }

        //Logic for checking if UserID exists in Users MicroService
        UserDTO? user = await _userMicroServiceClient.GetUserByUserId(orderAddRequest.UserID);
        if (user == null)
        {
            throw new ArgumentException("Invalid User");
        }

        //Convert data from OrderAddRequest to Order
        Order orderInput = _mapper.Map<Order>(orderAddRequest);

        //Generate Values
        foreach(OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }
        orderInput.TotalBill=orderInput.OrderItems.Sum(temp=>temp.TotalPrice);

        //Invoke Repository
        Order? addedOrder = await _orderRepository.AddOrder(orderInput);
        if (addedOrder != null)
        {
            return null;
        }

        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);
        return addedOrderResponse;


    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        //Check if Order Add Request is null or not
        if (orderUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(orderUpdateRequest));
        }

        //Validate OrderRequest using Fluent Validation
        ValidationResult orderUpdateRequestValidationResult = await _orderUpdateRequestValidator.ValidateAsync(orderUpdateRequest);
        if (!orderUpdateRequestValidationResult.IsValid)
        {
            string errors = string.Join(",", orderUpdateRequestValidationResult.Errors.Select
                (temp => temp.ErrorMessage));
            throw new ArgumentException(errors);

        }

        //Validate OrderItems using Fluent validation
        foreach (OrderItemUpdateRequest orderItemUpdateRequest in orderUpdateRequest.OrderItems)
        {
            ValidationResult orderItemUpdateRequestValidationResult = await _orderItemUpdateRequestValidator.ValidateAsync(orderItemUpdateRequest);
            if (!orderItemUpdateRequestValidationResult.IsValid)
            {
                string errors = string.Join(",", orderItemUpdateRequestValidationResult.Errors.Select
                    (temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }

        }

        //Logic for checking if UserID exists in Users MicroService
        UserDTO? user = await _userMicroServiceClient.GetUserByUserId(orderUpdateRequest.UserID);
        if (user == null)
        {
            throw new ArgumentException("Invalid User");
        }

        //Convert data from OrderUpdateRequest to Order
        Order orderInput = _mapper.Map<Order>(orderUpdateRequest);

        //Generate Values
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }
        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);

        //Invoke Repository
        Order? updatedOrder = await _orderRepository.UpdateOrder(orderInput);
        if (updatedOrder != null)
        {
            return null;
        }

        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(updatedOrder);
        return addedOrderResponse;
    }

    public async Task<bool> DeleteOrder(Guid OrderId)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq
            (temp => temp.OrderID, OrderId);
        Order? existingOrder = await _orderRepository.GetOrderByCondition(filter);
        if(existingOrder == null)
        {
               return false;
        }

        bool isDeleted = await _orderRepository.DeleteOrder(OrderId);
        
            return isDeleted;
        
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        Order? order = await _orderRepository.GetOrderByCondition(filter);
        if (order == null)
        {
            return null;
        }
        OrderResponse? orderResponse = _mapper.Map<OrderResponse>(order);
        return orderResponse;
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        IEnumerable<Order?> orders = await _orderRepository.GetOrdersByCondition(filter);
       
        IEnumerable<OrderResponse?> orderResponse = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return orderResponse.ToList();
    }
    public async Task<List<OrderResponse?>> GetOrders()
    {
        IEnumerable<Order?> orders = await _orderRepository.GetOrders();

        IEnumerable<OrderResponse?> orderResponse = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return orderResponse.ToList();
    }
}
