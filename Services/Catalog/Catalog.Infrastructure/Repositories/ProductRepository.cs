using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specification;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    //context is needed in this class

    public ICatalogContext _context { get; }
    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    async Task<Pagination<Product>> IProductRepository.GetProducts(CatalogSpecParam specParam)
    {
        #region Old Logic without pagination
        //Find(x=>true)this will return the whole collection
        //return await _context.Products.Find(x=>true).ToListAsync();
        #endregion
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        if (!string.IsNullOrEmpty(specParam.Search))
        {
            filter = filter & builder.Where(x => x.Name.ToLower().Contains(specParam.Search.ToLower()));
        }
        if (!string.IsNullOrEmpty(specParam.BrandId))
        {
            var brandFilter = filter & builder.Eq(x => x.Brands.Id, specParam.BrandId);
            filter &= brandFilter;
        }
        if (!string.IsNullOrEmpty(specParam.TypeId))
        {
            var typeFilter = filter & builder.Eq(x => x.Types.Id, specParam.TypeId);
            filter &= typeFilter;
        }
        var totalItems = await _context.Products.CountDocumentsAsync(filter);
        var data = await DataFilter(specParam, filter);
        return new Pagination<Product>(
            specParam.PageIndex,
            specParam.PageSize,
            (int)totalItems,
            data
        );
    }



    async Task<Product> IProductRepository.GetProduct(string Id)
    {
        return await _context.Products.Find(x => x.Id == Id).FirstOrDefaultAsync();
    }

    async Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string name)
    {
        return await _context.Products.FindAsync(x => x.Name.ToLower() == name.ToLower()).Result.ToListAsync();
    }

    async Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string brand)
    {
        return await _context.Products.FindAsync(x => x.Brands.Name.ToLower() == brand.ToLower()).Result.ToListAsync();
    }

    async Task<Product> IProductRepository.CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    async Task<bool> IProductRepository.UpdateProduct(Product product)
    {
        var updatedProduct = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;// if true then data is updated
    }

    async Task<bool> IProductRepository.DeleteProduct(string Id)
    {
        var deletedProduct = await _context.Products.DeleteOneAsync(x => x.Id == Id);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;// if true then data is deleted
    }

    async Task<IEnumerable<ProductBrand>> IBrandRepository.GetAllBrands()
    {
        return await _context.Brands.FindAsync(x => true).Result.ToListAsync();
    }

    async Task<IEnumerable<ProductType>> ITypesRepository.GetAllTypes()
    {
        return await _context.Types.FindAsync(x => true).Result.ToListAsync();
    }

    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParam specParam, FilterDefinition<Product> filter)
    {
        var sortDefs = Builders<Product>.Sort.Ascending("Name");//Default
        if (!string.IsNullOrEmpty(specParam.Sort))
        {
            switch (specParam.Sort)
            {
                case "priceAsc":
                    sortDefs = Builders<Product>.Sort.Ascending(x => x.Price);
                    break;
                case "priceDesc":
                    sortDefs = Builders<Product>.Sort.Descending(x => x.Price);
                    break;
                case "nameAsc":
                    sortDefs = Builders<Product>.Sort.Ascending(x => x.Name);
                    break;
                case "nameDesc":
                    sortDefs = Builders<Product>.Sort.Descending(x => x.Name);
                    break;
                default:
                    sortDefs = Builders<Product>.Sort.Ascending(x => x.Name);
                    break;
            }
        }
        return await _context.Products.Find(filter).Sort(sortDefs).Skip(specParam.PageSize * (specParam.PageIndex - 1)).
                           Limit(specParam.PageSize).ToListAsync();
    }
}
