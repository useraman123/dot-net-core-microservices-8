using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handler;

public class GetAllBrandsHandler:IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>> 
{
    private readonly IBrandRepository _brandRepo;
    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepo = brandRepository;
    }


    /// <summary>
         //First Approach: Direct DI-based Mapper (_mapper)
        // var brandResponseList =_mapper.Map<IList<ProductBrand>,IList<BrandResponse>>(brands.ToList());
        // Lazy Mapping   Lazy Singleton Mapper (ProductsMapper.Mapper)
    /// </summary>
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery query,CancellationToken cancellationToken)
    {
        var brands = await _brandRepo.GetAllBrands();

        //var brandResponseList =ProductsMapper.Mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brands.ToList());
        var brandResponseList = ProductsMapper.Mapper.Map<IList<BrandResponse>>(brands.ToList());
        return brandResponseList;
    }
}
