using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Application.Mappers;
using Order.Core.Entities;
using Order.Core.Repositories;

namespace Order.Application.Handlers;

public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<CheckoutOrderHandler> _logger;
    public CheckoutOrderHandler(IOrderRepository repository, ILogger<CheckoutOrderHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = OrderMapper.Mapper.Map<OrderEntity>(request);
        var generatedOrder = await _repository.AddAsync(orderEntity);
        _logger.LogInformation($"Order with id {generatedOrder.Id} successfully created");
        return generatedOrder.Id;
    }
}
