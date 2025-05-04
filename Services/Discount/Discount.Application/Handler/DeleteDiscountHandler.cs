using Discount.Application.Command;
using Discount.Application.Reponses;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handler;

public class DeleteDiscountHandler:IRequestHandler<DeleteDiscountCommand,bool>
{
    private readonly IDiscount _discount;

    public DeleteDiscountHandler(IDiscount discount)
    {
        _discount = discount;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _discount.DeleteDiscount(request.productName);
        return deleted;
    }
}
