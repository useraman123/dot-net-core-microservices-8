using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductByName(string name):IRequest<IList<ProductResponse>>;
