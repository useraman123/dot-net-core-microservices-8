using Catalog.Application.Command;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    public DeleteProductHandler(IProductRepository productRepository)
    {
        _repository = productRepository;
    }
    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        return await _repository.DeleteProduct(command.Id);
    }
}
