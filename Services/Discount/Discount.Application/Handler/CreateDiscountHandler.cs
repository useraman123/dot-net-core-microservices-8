using Discount.Application.Command;
using Discount.Application.Mapper;
using Discount.Application.Reponses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handler;

public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
{
    private readonly IDiscount _discount;

    public CreateDiscountHandler(IDiscount discount)
    {
        _discount=discount;
    }
    public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var createrequest = DiscountMapper.Mapper.Map<Coupon>(request);
        await _discount.CreateDiscount(createrequest);
        var response = DiscountMapper.Mapper.Map<CouponModel>(request);
        return response;
    }
}
