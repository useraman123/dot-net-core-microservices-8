using Catalog.Application.Command;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class UpdateProductHandler:IRequestHandler<UpdateProductCommand,bool>
{
    private readonly IProductRepository _repository;
    public UpdateProductHandler(IProductRepository productRepository)
    {
        _repository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var updatedProduct = await _repository.UpdateProduct(new Product
        {
            Id = command.Id,    
            Name = command.Name,
            Description = command.Description,
            ImageFile= command.ImageFile,
            Price= command.Price,
            Brands= command.Brands,
            Types=command.Types,
            Summary=command.Summary,
        });
        return updatedProduct;
    }

}
