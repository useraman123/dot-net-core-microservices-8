using Discount.Application.Reponses;
using Discount.Core.Entities;
using MediatR;

namespace Discount.Application.Queries;

public record GetDiscountQuery(string productName):IRequest<CouponModel>;