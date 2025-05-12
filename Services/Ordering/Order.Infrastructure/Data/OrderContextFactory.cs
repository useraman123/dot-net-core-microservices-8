using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Order.Infrastructure.Data;

public class OrderContextFactory:IDesignTimeDbContextFactory<OrderContext>
{

    public OrderContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=Anya;Database=OrderDb;TrustServerCertificate=True;Trusted_Connection=True;Encrypt=True;Integrated Security=True;";
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        //optionsBuilder.UseSqlServer("Data Source=OrderDb");
        optionsBuilder.UseSqlServer(connectionString);
        return new OrderContext(optionsBuilder.Options);
    }
}
