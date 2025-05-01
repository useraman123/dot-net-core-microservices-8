using Basket.Application.Reponses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Command;

public record CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; }
    public CreateShoppingCartCommand(string userName , List<ShoppingCartItem> carts)
    {
        UserName = userName;
        Items = carts;
    }
}