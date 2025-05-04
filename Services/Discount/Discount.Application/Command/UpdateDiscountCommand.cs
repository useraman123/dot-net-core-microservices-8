using Discount.Application.Reponses;
using MediatR;

namespace Discount.Application.Command;

public record UpdateDiscountCommand(string Id,string productName,string Description,int Amount):IRequest<CouponModel>;