using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class GetAllTypeHandler:IRequestHandler<GetAllTypeQuery,IList<TypeResponse>>
{
    private readonly ITypesRepository _typeRepo;
    public GetAllTypeHandler(ITypesRepository typesRepository)
    {
        _typeRepo= typesRepository;
    }
    public async Task<IList<TypeResponse>> Handle(GetAllTypeQuery query,CancellationToken cancellationToken)
    {
        var getAllType = await _typeRepo.GetAllTypes();
        var response = ProductsMapper.Mapper.Map<IList<TypeResponse>>(getAllType);
        return response;
    }
}
