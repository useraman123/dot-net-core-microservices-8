using Discount.Application.Reponses;
using MediatR;

namespace Discount.Application.Command;

public record CreateDiscountCommand(string ProductName,string Description,int Amount) :IRequest<CouponModel>;