using Basket.Application.Reponses;
using MediatR;

namespace Basket.Application.Queries;

public record GetBasketByUserNameQuery:IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }
    public GetBasketByUserNameQuery(string userName)
    {
        UserName=userName;
    }
}
