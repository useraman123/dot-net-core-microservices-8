using Catalog.Application.Responses;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Queries;

public record GetAllProductsQuery(CatalogSpecParam _specParam) : IRequest<Pagination<ProductResponse>>;
//{
//    public CatalogSpecParam _specParam { get; set; }
//    public GetAllProductsQuery(CatalogSpecParam specParam)
//    {
//        _specParam=specParam;
//    }
//}
