using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductById(string Id):IRequest<ProductResponse>;
