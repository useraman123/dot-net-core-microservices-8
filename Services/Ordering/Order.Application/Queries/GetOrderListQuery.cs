using MediatR;
using Order.Application.Responses;

namespace Order.Application.Queries;

public record GetOrderListQuery(string userName):IRequest<List<OrderResponse>>;
