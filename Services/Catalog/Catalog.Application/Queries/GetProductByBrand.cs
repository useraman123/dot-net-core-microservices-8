using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductByBrand(string brand):IRequest<IList<ProductResponse>>;
