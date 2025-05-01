using Basket.Application.Command;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handler;

public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand,Unit>
{
    private readonly IBasketRepository _basket;
    public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basket = basketRepository;
    }
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _basket.DeleteBasket(request.UserName);
        return Unit.Value;
    }
}
