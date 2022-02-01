using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.Orders.Domain.Shared.ViewModels;
using MongoDB.Driver;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class UtilityOrderMongoDbContext
    {
        private const string OrdersCollectionName = "Orders";
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        private IMongoCollection<OrderFulFilledVM>? _projects;

        public UtilityOrderMongoDbContext(IConfiguration configuration)
        {
            _client = new MongoClient(MongoClientSettings.FromConnectionString(configuration["MongoDB:Server"]));
            _database = _client.GetDatabase(configuration["MongoDB:DBName"]);
        }

        public IMongoCollection<OrderFulFilledVM> Orders
        {
            get
            {
                if (_projects == null)
                    _projects = _database.GetCollection<OrderFulFilledVM>(OrdersCollectionName);
                return _projects;
            }
        }
    }
}
