using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandsCollection)
    {
        // we are making sure if there is nothing 
        bool checkBrands = brandsCollection.Find(x => true).Any();
        // path should be constructed using the dynamic setting
        string path = Path.Combine("Data", "SeedData", "brands.json");
        if (!checkBrands)
        {
            var brandData = File.ReadAllText(path);
            // while deserilazing we are converting the json to the model format
            var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            if (brand != null)
            {
                foreach (var item in brand)
                {
                    brandsCollection.InsertOneAsync(item);
                }
            }
        }
    }

    #region ForDebugging Just in case
    // but make sure mongo is running on docker
    //public static void SeedData(IMongoCollection<ProductBrand> brandsCollection)
    //{
    //    // we are making sure if there is nothing 
    //    bool checkBrands = brandsCollection.Find(x => true).Any();
    //    // path should be constructed using the dynamic setting
    //    //string path = Path.Combine("Data", "SeedData", "brands.json");

    //    if (!checkBrands)
    //    {
    //        //var brandData = File.ReadAllText(path);
    //        // while deserilazing we are converting the json to the model format
    //        var brandData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json");
    //        var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
    //        if (brand != null)
    //        {
    //            foreach (var item in brand)
    //            {
    //                brandsCollection.InsertOneAsync(item);
    //            }
    //        }
    //    }
    //}
    #endregion
}
