using AutoMapper;
using Basket.Application.Reponses;
using Basket.Core.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Mappers;

public class BasketMappingProfile: Profile
{
    public BasketMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        //for masstransit
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}
