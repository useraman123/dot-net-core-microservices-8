using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Order.Application.Commands;

namespace Order.API.EventBusConsumer;

public class BasketOrderingConsumer(
    IMediator _mediator,
    IMapper _mapper,
    ILogger<BasketOrderingConsumer> _logger
    ) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}", context.Message.CorrelationId);
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await _mediator.Send(command);
        _logger.LogInformation("Basket Checkout Event Completed");
    }
}
