using MediatR;
using Order.Application.Mappers;
using Order.Application.Queries;
using Order.Application.Responses;
using Order.Core.Repositories;

namespace Order.Application.Handlers;

public class GetOrderListHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
{
    private IOrderRepository _repo;
    public GetOrderListHandler(IOrderRepository repository)
    {
        _repo = repository;
    }


    public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var order = await _repo.GetOrdersByUserName(request.userName);
        var response = OrderMapper.Mapper.Map<List<OrderResponse>>(order);
        return response;
    }
}
