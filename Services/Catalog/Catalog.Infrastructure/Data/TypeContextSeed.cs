using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public static class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typesCollection)
    {
        // we are making sure if there is nothing 
        bool checkTypes = typesCollection.Find(x => true).Any();
        // path should be constructed using the dynamic setting
        string path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            var typesData = File.ReadAllText(path);
            // while deserilazing we are converting the json to the model format
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if (types != null)
            {
                foreach (var item in types)
                {
                    typesCollection.InsertOneAsync(item);
                }
            }
        }
    }

    #region ForDebugging Just in case
    //public static void SeedData(IMongoCollection<ProductType> typesCollection)
    //{
    //    // we are making sure if there is nothing 
    //    bool checkTypes = typesCollection.Find(x => true).Any();
    //    // path should be constructed using the dynamic setting
    //    //string path = Path.Combine("Data", "SeedData", "types.json");
    //    if (!checkTypes)
    //    {
    //        //var typesData = File.ReadAllText(path);
    //        // while deserilazing we are converting the json to the model format
    //        var typesData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
    //        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
    //        if (types != null)
    //        {
    //            foreach (var item in types)
    //            {
    //                typesCollection.InsertOneAsync(item);
    //            }
    //        }
    //    }
    //}
    #endregion
}
