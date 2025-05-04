using Basket.Application.Reponses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Command;


public record CreateShoppingCartCommand(
    string UserName,
    List<ShoppingCartItem> Items
) : IRequest<ShoppingCartResponse>;
