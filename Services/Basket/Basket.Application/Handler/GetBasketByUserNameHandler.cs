using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Reponses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handler;

public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basket;
    public GetBasketByUserNameHandler(IBasketRepository basket)
    {
        _basket=basket;
    }
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basket.GetBasket(request.UserName);
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}
