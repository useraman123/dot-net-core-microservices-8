using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class GetProductByNameHandler:IRequestHandler<GetProductByName,IList<ProductResponse>>
{
    private readonly IProductRepository _repo;
    public GetProductByNameHandler(IProductRepository productRepository)
    {
        _repo = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByName query,CancellationToken cancellationToken)
    {
        var productData = await _repo.GetProductsByName(query.name);
        var response = ProductsMapper.Mapper.Map<IList<ProductResponse>>(productData);
        return response;
    }
}
