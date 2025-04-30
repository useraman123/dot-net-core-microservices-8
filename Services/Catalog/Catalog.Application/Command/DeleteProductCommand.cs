using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Command;

public record DeleteProductCommand(string Id):IRequest<bool>;
