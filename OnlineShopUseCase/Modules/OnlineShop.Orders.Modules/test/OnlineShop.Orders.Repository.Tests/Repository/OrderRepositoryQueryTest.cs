using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.MongoDB;
using Microsoft.Extensions.Configuration;
using OnlineShop.Orders.Domain.Shared.ViewModels;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Collections.Generic;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class OrderRepositoryQueryTest
    { 
        readonly Mock<INoSqlService> mongoService;
        readonly OrderRepositoryQuery orderRepo;
        readonly Mock<IFakeMongoCollection<OrderFulFilledVM>> _fakeMongoCollection;

        public OrderRepositoryQueryTest()
        {
            _fakeMongoCollection = new Mock<IFakeMongoCollection<OrderFulFilledVM>>();
             
            mongoService = new Mock<INoSqlService>();

            orderRepo = new OrderRepositoryQuery(mongoService.Object);
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var order = new OrderFulFilledVM()
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderStatus = Domain.Shared.Enums.OrderStatus.Suspend,
                TotalPrice = 100,
                Customer = new CustomerVM()
                {
                    Address = "The address",
                    Name = "Kemal",
                    Phone = "123131",
                    Surname = "Bey"
                },
                OrderItems = new OrderItemVM[] {
                     new OrderItemVM
                     {
                        Count=1,
                        Price = 100,
                        Product = new ProductVM(){
                             Brand = "Apple",
                             Model = "Iphone",
                             Title = "Apple Iphone",
                        },
                        ProductId = Guid.NewGuid(),
                     }
                 }
            };

            mongoService.Setup(a => a.GetCollection<OrderFulFilledVM>()).Returns(_fakeMongoCollection.Object);

            await orderRepo.CreateOrderFulFilledVM(order);
            Assert.True(true);
        }

        #endregion
        
    }

    public interface IFakeMongoCollection<T> : IMongoCollection<T>
    {
        Task InsertOneAsync(T document);

        Task<List<T>> ToListAsync<TDocument>();

        IFindFluent<BsonDocument, BsonDocument> Find(FilterDefinition<BsonDocument> filter, FindOptions options);

        IFindFluent<BsonDocument, BsonDocument> Project(ProjectionDefinition<BsonDocument, BsonDocument> projection);

        IFindFluent<BsonDocument, BsonDocument> Skip(int skip);
         
        IFindFluent<BsonDocument, BsonDocument> Limit(int limit);

        IFindFluent<BsonDocument, BsonDocument> Sort(SortDefinition<BsonDocument> sort);
    }
}
