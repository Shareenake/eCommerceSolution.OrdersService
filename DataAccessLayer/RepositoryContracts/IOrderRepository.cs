

using eCommerce.OrderService.DataAccessLayer.Entities;
using MongoDB.Driver;

namespace eCommerce.OrderService.DataAccessLayer.RepositoryContracts;

public interface IOrderRepository
{
    /// <summary>
    /// Retrieves all orders asynchronously
    /// </summary>
    /// <returns>Returns all orders from order collection</returns>
    Task<IEnumerable<Order>> GetOrders();

    /// <summary>
    /// Retrieves all orders based on the condition asynchronously
    /// </summary>
    /// <param name="filter">The condition to filter the data</param>
    /// <returns>Returns collection of matching orders</returns>
    Task<IEnumerable<Order>>GetOrdersByCondition(FilterDefinition<Order>filter);

    /// <summary>
    /// Retrieves the order base on the specified condition
    /// </summary>
    /// <param name="filter">The condition to filter the Order</param>
    /// <returns>Returns matching order</returns>
    Task<Order> GetOrderByCondition(FilterDefinition<Order> filter);

    /// <summary>
    /// Add a new order to the order collection asynchronously
    /// </summary>
    /// <param name="order">The order to be added</param>
    /// <returns>Returns the added Order object or null if unsuccessful</returns>
    Task<Order?>AddOrder(Order order);

    /// <summary>
    /// Updates an existing order asynchronously
    /// </summary>
    /// <param name="order">The order to be added</param>
    /// <returns>Returns the updated Order object or null if unsuccessful</returns>
    Task<Order?>UpdateOrder(Order order);

    /// <summary>
    /// Deletes the order asynchronously
    /// </summary>
    /// <param name="orderId">The orderId to be deleted</param>
    /// <returns>Returns true if success otherwise false</returns>
    Task<bool> DeleteOrder(Guid orderId);
}
