using MediatR;

namespace Order.Application.Commands;

public record DeleteOrderCommand(int id):IRequest<Unit>;
