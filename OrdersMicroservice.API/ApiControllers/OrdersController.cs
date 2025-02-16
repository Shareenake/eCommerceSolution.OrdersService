using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrderService.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersMicroService.API.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //GET: api/Orders
        [HttpGet]
        public async Task<IEnumerable<OrderResponse?>> Get()
        {
            List<OrderResponse?> orders = await _orderService.GetOrders();
            return orders;
        }

        //GET: api/Orders/search/orderId/{orderID}
        [HttpGet("/search/orderId/{orderID}")]
        public async Task<OrderResponse?> GetOrdersByOrderID(Guid orderID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq
                (temp => temp.OrderID, orderID);
            OrderResponse? order = await _orderService.GetOrderByCondition(filter);
            return order;
        }

        //GET: api/Orders/search/productid/{productID}
        [HttpGet("/search/productid/{productID}")]
        public async Task<List<OrderResponse?>> GetOrdersByProductID(Guid productID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.ElemMatch
                (temp => temp.OrderItems,
                Builders<OrderItem>.Filter.Eq(tempProduct => tempProduct.ProductID, productID));
            List<OrderResponse?> orders = await _orderService.GetOrdersByCondition(filter);
            return orders.ToList();
        }

        //GET: api/Orders/search/orderdate/{orderDate}
        [HttpGet("/search/orderdate/{orderDate}")]
        public async Task<List<OrderResponse?>> GetOrdersByDate(DateTime orderDate)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq
                (temp => temp.OrderDate.ToString("yyyy-MM-dd"), orderDate.ToString("yyyy-MM-dd"));
            List<OrderResponse?> orders = await _orderService.GetOrdersByCondition(filter);
            return orders.ToList();
        }

        //GET: api/Orders/search/userid/{userID}
        [HttpGet("/search/orderdate/{userID}")]
        public async Task<List<OrderResponse?>> GetOrdersByUserID(Guid userID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq
                (temp => temp.UserID, userID);
            List<OrderResponse?> orders = await _orderService.GetOrdersByCondition(filter);
            return orders.ToList();
        }

        //POST : api/Orders/
        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                return BadRequest("Invalid Order data");
            }

            OrderResponse? orderResponse = await _orderService.AddOrder(orderAddRequest);

            if (orderResponse == null)
            {
                return Problem("Error in adding Product");
            }
            return Created($"api/Orders/search/orderid/{orderResponse.OrderID}", orderResponse);
        }

        //POST : api/Orders/{orderID}
        [HttpPut("{orderID}")]
        public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest("Invalid Order data");
            }
            if (orderID != orderUpdateRequest.OrderID)
            {
                return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
            }

            OrderResponse? orderResponse = await _orderService.UpdateOrder(orderUpdateRequest);

            if (orderResponse == null)
            {
                return Problem("Error in updating Product");
            }
            return Ok(orderResponse);
        }

        //DELETE : api/Orders/{orderID}
        [HttpDelete("{orderID}")]
        public async Task<IActionResult> Delete(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                return BadRequest("Invalid Order ID");
            }


            bool isDeleted = await _orderService.DeleteOrder(orderID);

            if (!isDeleted)
            {
                return Problem("Error in deleting Product");
            }
            return Ok(isDeleted);
        }
    }
}
