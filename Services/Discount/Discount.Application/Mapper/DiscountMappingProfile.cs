using AutoMapper;
using Discount.Application.Command;
using Discount.Application.Reponses;
using Discount.Core.Entities;

namespace Discount.Application.Mapper;

public class DiscountMappingProfile:Profile
{
    public DiscountMappingProfile()
    {
        // Mapper Source -> Destination 
        CreateMap<Coupon, CouponModel>().ReverseMap();
        CreateMap<CreateDiscountCommand, Coupon>().ReverseMap();
        CreateMap<CreateDiscountCommand, CouponModel>().ReverseMap();
        CreateMap<UpdateDiscountCommand, Coupon>().ReverseMap();
        CreateMap<UpdateDiscountCommand, CouponModel>().ReverseMap();
    }
}
