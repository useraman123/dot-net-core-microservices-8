using Discount.Application.Mapper;
using Discount.Application.Queries;
using Discount.Application.Reponses;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handler;

public class GetDiscountHandler(IDiscount _discount, ILogger<GetDiscountHandler> _logger) : IRequestHandler<GetDiscountQuery, CouponModel>
{
    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discount.GetDiscount(request.productName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for {request.productName} not found"));
        }
        var result = DiscountMapper.Mapper.Map<CouponModel>(coupon);
        _logger.LogInformation($"Discounted fetched for {request.productName}");
        return result;
    }
}
