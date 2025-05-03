using AutoMapper;
using Discount.Application.Reponses;
using Discount.Core.Entities;

namespace Discount.Application.Mapper;

public class DiscountMappingProfile:Profile
{
    public DiscountMappingProfile()
    {
        // Mapper Source -> Destination 
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}
