using Order.Core.Entities;


namespace Order.Core.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
}
