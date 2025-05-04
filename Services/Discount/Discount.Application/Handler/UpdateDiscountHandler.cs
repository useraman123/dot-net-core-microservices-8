using Discount.Application.Command;
using Discount.Application.Mapper;
using Discount.Application.Reponses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handler;

public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
{
    private readonly IDiscount _discount;

    public UpdateDiscountHandler(IDiscount discount)
    {
        _discount = discount;
    }
    
    public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var updaterequest = DiscountMapper.Mapper.Map<Coupon>(request);
        await _discount.UpdateDiscount(updaterequest);
        var response = DiscountMapper.Mapper.Map<CouponModel>(updaterequest);
        return response;
    }
}
