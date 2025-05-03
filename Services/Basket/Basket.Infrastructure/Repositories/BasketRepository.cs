using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    public BasketRepository(IDistributedCache cache)
    {
        _redisCache = cache;
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }
        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpsertBasket(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        return await GetBasket(basket.UserName);
    }
    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    

    
}
