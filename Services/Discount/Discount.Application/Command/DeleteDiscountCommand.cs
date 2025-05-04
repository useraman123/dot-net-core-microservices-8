using Discount.Application.Reponses;
using MediatR;

namespace Discount.Application.Command;

public record DeleteDiscountCommand(string productName):IRequest<bool>;