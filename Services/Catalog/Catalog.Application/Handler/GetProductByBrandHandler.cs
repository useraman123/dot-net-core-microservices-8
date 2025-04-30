using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class GetProductByBrandHandler:IRequestHandler<GetProductByBrand,IList<ProductResponse>>
{
    private readonly IProductRepository _repository;
    public GetProductByBrandHandler(IProductRepository productRepository)
    {
        _repository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductByBrand query, CancellationToken cancellationToken)
    {
        var productData = await _repository.GetProductsByBrand(query.brand);
        var response = ProductsMapper.Mapper.Map<IList<ProductResponse>>(productData);
        return response;
    }

}
