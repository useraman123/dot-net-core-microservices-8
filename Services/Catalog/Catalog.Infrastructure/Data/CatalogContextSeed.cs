using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productsCollection)
    {
        // we are making sure if there is nothing 
        bool checkProducts = productsCollection.Find(x => true).Any();
        // path should be constructed using the dynamic setting
        string path = Path.Combine("Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText(path);
            // while deserilazing we are converting the json to the model format
            var product = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (product != null)
            {
                foreach (var item in product)
                {
                    productsCollection.InsertOneAsync(item);
                }
            }
        }
    }

    #region ForDebugging Just in case
    //public static void SeedData(IMongoCollection<Product> productsCollection)
    //{
    //    // we are making sure if there is nothing 
    //    bool checkProducts = productsCollection.Find(x => true).Any();
    //    // path should be constructed using the dynamic setting
    //    //string path = Path.Combine("Data", "SeedData", "products.json");
    //    if (!checkProducts)
    //    {
    //        //var productsData = File.ReadAllText(path);
    //        // while deserilazing we are converting the json to the model format
    //        var productsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/products.json");
    //        var product = JsonSerializer.Deserialize<List<Product>>(productsData);
    //        if (product != null)
    //        {
    //            foreach (var item in product)
    //            {
    //                productsCollection.InsertOneAsync(item);
    //            }
    //        }
    //    }
    //}
    #endregion
}
