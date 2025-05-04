using Discount.Application.Mapper;
using Discount.Application.Queries;
using Discount.Application.Reponses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handler;

public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscount _discount;
    public GetDiscountHandler(IDiscount discount)
    {
        _discount = discount;
    }
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discount.GetDiscount(request.productName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for {request.productName} not found"));
        }
        var result = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        return result;
    }
}
