using Basket.Application.Reponses;
using MediatR;

namespace Basket.Application.Queries;

public record GetBasketByUserNameQuery(string UserName) :IRequest<ShoppingCartResponse>;
