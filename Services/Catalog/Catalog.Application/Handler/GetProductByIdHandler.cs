using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class GetProductByIdHandler:IRequestHandler<GetProductById,ProductResponse>
{
    private readonly IProductRepository _repo;
    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _repo = productRepository;
    }

    public async Task<ProductResponse> Handle(GetProductById query,CancellationToken cancellationToken)
    {
        var product = await _repo.GetProduct(query.Id);
        var response = ProductsMapper.Mapper.Map<ProductResponse>(product);
        return response;
    }
}
