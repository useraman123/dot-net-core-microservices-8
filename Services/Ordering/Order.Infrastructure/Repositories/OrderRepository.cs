using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Core.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
{

    public OrderRepository(OrderContext _context):base(_context)
    {

    }
    public async Task<IEnumerable<OrderEntity>> GetOrdersByUserName(string userName)
    {
        var orderList =await _context.Orders.Where(x=>x.UserName == userName).ToListAsync();
        return orderList;
    }
}
