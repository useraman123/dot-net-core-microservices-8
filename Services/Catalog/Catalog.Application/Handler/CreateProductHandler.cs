using Catalog.Application.Command;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _repository;
    public CreateProductHandler(IProductRepository productRepository)
    {
        _repository = productRepository;
    }
    public async Task<ProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var entity = ProductsMapper.Mapper.Map<Product>(command);
        if(entity == null)
        {
            throw new ApplicationException("There is an issue in mapping while creating product");
        }
        var newProduct = await _repository.CreateProduct(entity);
        var response = ProductsMapper.Mapper.Map<ProductResponse>(newProduct);
        return response;
    }
}
