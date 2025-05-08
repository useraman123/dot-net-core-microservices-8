using Order.Core.Entities;

namespace Order.Core.Repositories;

public interface IOrderRepository:IAsyncRepository<OrderEntity>
{
    Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName);
}
