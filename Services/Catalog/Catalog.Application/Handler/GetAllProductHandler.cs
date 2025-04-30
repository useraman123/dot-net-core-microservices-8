using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specification;
using MediatR;

namespace Catalog.Application.Handler;

public class GetAllProductHandler:IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _repository;
    public GetAllProductHandler(IProductRepository productRepository)
    {
        _repository=productRepository;
    }
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery query,CancellationToken cancellationToken)
    {
        var productList = await _repository.GetProducts(query._specParam);
        var response = ProductsMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
        return response;
    }
}
