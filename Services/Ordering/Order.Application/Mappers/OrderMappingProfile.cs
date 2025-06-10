using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Commands;
using Order.Application.Responses;
using Order.Core.Entities;

namespace Order.Application.Mappers;

public class OrderMappingProfile:Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderEntity,OrderResponse>().ReverseMap();
        CreateMap<CheckoutOrderCommand, OrderEntity>().ReverseMap();
        CreateMap<UpdateOrderCommand, OrderEntity>().ReverseMap();
        CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
    }
}
