using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Application.Exception;
using Order.Core.Entities;
using Order.Core.Repositories;

namespace Order.Application.Handlers;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<DeleteOrderHandler> _logger;


    public DeleteOrderHandler(IOrderRepository repository, ILogger<DeleteOrderHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.id);
        if (order == null)
        {
            throw new OrderNotFoundException(nameof(OrderEntity), request.id);
        }
        await _repository.DeleteAsync(order);
        _logger.LogInformation($"Order with Id = {order.Id} deleted successfully");
        return Unit.Value;
    }
}
