using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace OnlineShop.Orders.MongoDB
{
    public interface INoSqlService
    {
        IMongoCollection<T> GetCollection<T>();
    }
}