using AutoMapper;
using Order.Application.Responses;
using Order.Core.Entities;

namespace Order.Application.Mappers;

public class OrderMappingProfile:Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderEntity,OrderResponse>().ReverseMap();
    }
}
