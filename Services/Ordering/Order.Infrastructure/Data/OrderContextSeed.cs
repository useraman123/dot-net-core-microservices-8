using Microsoft.Extensions.Logging;
using Order.Core.Entities;
using System.Collections;

namespace Order.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (!context.Orders.Any())
        {
            context.Orders.AddRange(GetOrders());
            await context.SaveChangesAsync();
            logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!");
        }
    }
        private static IEnumerable<OrderEntity> GetOrders()
        {
                return new List<OrderEntity>
                { 
                    new()
                    {
                        UserName="aman",
                        FirstName="Aman",
                        LastName="Kumar",
                        EmailAddress="amank@yopmail.com",
                        AddressLine="vijay nagar pratap vihar ghaziabad",
                        Country="India",
                        TotalPrice=100,
                        State="UP",
                        ZipCode="201009",
                        CardName="Visa",
                        CardNumber="1235897654321",
                        CreatedBy="aman",
                        Expiration="12/25",
                        Cvv="256",
                        PaymentMethod=1,
                        LastModifiedBy="aman",
                        LastModifiedDate=DateTime.UtcNow
                    }
                };
        }
}
