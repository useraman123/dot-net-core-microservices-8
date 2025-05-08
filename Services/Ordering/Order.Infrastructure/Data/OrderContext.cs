using Microsoft.EntityFrameworkCore;
using Order.Core.Common;
using Order.Core.Entities;

namespace Order.Infrastructure.Data;

public class OrderContext:DbContext
{
    public OrderContext(DbContextOptions<OrderContext> contextOptions):base(contextOptions)
    {
        
    }

    public DbSet<OrderEntity> Orders { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangeOnSuccess,CancellationToken cancellationToken)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate=DateTime.UtcNow;
                    entry.Entity.CreatedBy = "Aman";
                    break;

                case EntityState.Modified:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "Aman";
                    break;

            }
        }
        return base.SaveChangesAsync(acceptAllChangeOnSuccess, cancellationToken);
    }
}
