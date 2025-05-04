using Basket.Application.Command;
using Basket.Application.Mappers;
using Basket.Application.Reponses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handler;

public class CreateShoppingCartHandler:IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basket;
    public CreateShoppingCartHandler(IBasketRepository basket)
    {
        _basket = basket;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // TODO: WIll Be integrating Discount service here 
        var shoppingCart = await _basket.UpsertBasket(new Core.Entities.ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items,
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}
