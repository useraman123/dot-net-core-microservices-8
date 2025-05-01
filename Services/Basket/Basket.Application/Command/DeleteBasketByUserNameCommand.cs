using MediatR;

namespace Basket.Application.Command;

public record DeleteBasketByUserNameCommand:IRequest<Unit>
{
    public string UserName { get; set; }
    public DeleteBasketByUserNameCommand(string userName)
    {
        UserName= userName;
    }
}
