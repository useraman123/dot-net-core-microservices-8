using Discount.Application.Queries;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handler;

public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, Coupon>
{
    private readonly IDiscount _discount;
    public GetDiscountHandler(IDiscount discount)
    {
        _discount = discount;
    }
    public Task<Coupon> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
