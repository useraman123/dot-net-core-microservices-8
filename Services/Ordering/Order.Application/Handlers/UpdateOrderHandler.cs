using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Application.Exception;
using Order.Application.Mappers;
using Order.Core.Entities;
using Order.Core.Repositories;

namespace Order.Application.Handlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    public UpdateOrderHandler(IOrderRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _repository.GetByIdAsync(request.id);
        if (orderToUpdate == null)
        {
            throw new OrderNotFoundException(nameof(OrderEntity), request.id);
        }
        OrderMapper.Mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(OrderEntity));
        _logger.LogInformation($"Order with Id {orderToUpdate.Id} updated successfully");
        return Unit.Value;
    }
}
