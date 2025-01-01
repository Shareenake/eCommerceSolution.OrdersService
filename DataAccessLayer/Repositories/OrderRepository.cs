

using eCommerce.OrderService.DataAccessLayer.Entities;
using eCommerce.OrderService.DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;

namespace eCommerce.OrderService.DataAccessLayer.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order?> AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }
}
