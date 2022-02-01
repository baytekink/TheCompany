using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace OnlineShop.Orders.MongoDB
{
    public class MongoDbService : INoSqlService
    {
        readonly IMongoDatabase database;
         
        public MongoDbService(IConfiguration configuration)
        {            
            var client = new MongoClient(MongoClientSettings.FromConnectionString(configuration["MongoDB:Server"]));
            database = client.GetDatabase(configuration["MongoDB:DBName"]);
        }

        public IMongoCollection<T> GetCollection<T>() => database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}