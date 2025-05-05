using Basket.Application.Command;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Reponses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handler;

public class CreateShoppingCartHandler:IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basket;
    //consuming the discount Grpc service
    private readonly DiscountGrpcService _discountGrpcService;
    public CreateShoppingCartHandler(IBasketRepository basket, DiscountGrpcService discountGrpcService)
    {
        _basket = basket;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // TODO: WIll Be integrating Discount service here 
        foreach (var item in request.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price = item.Price - coupon.Amount;
        }
        var shoppingCart = await _basket.UpsertBasket(new Core.Entities.ShoppingCart
        {
            UserName = request.UserName,
            Items = request.Items,
        });
        var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
        return shoppingCartResponse;
    }
}
