
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace eCommerce.OrderService.BusinessLogicLayer.ServiceContracts
{
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves list of orders from orders repository
        /// </summary>
        /// <returns>Returns a list of OrderResponse object</returns>
        Task<List<OrderResponse?>> GetOrders();

        /// <summary>
        /// Returns list of orders matching with given condition
        /// </summary>
        /// <param name="filter">Condition to check</param>
        /// <returns>Returns matching orders as response objects</returns>
        Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter);

        /// <summary>
        /// Returns the order matching with given condition
        /// </summary>
        /// <param name="filter">Condition to check</param>
        /// <returns>Returns matching order as response objects</returns>
        Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter);

        /// <summary>
        /// Adds order into the collection using order repository
        /// </summary>
        /// <param name="orderAddRequest">Order to insert</param>
        /// <returns>Returns OrderResponse object that contains order details 
        /// after inserting; or returns null if insertion fails</returns>
        Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);
        /// <summary>
        /// Updates order based on the Order id
        /// </summary>
        /// <param name="orderUpdateRequest">Order data to update </param>
        /// <returns>Returns order object after successful updation; otherwise null</returns>
        Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
        /// <summary>
        /// Deletes an existing order based on the order id
        /// </summary>
        /// <param name="orderId">Order id to search and delete</param>
        /// <returns>Return true if success otherwise false</returns>
        Task<bool> DeleteOrder(Guid orderId);
    }
}
