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
        readonly Mock<IMongoCollection<OrderFulFilledVM>> _moqMongoCollection;

        public OrderRepositoryQueryTest()
        {
            _moqMongoCollection = new Mock<IMongoCollection<OrderFulFilledVM>>();

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
             
            _moqMongoCollection.Setup(p => p.InsertOneAsync(order, null, default));
            mongoService.Setup(a => a.GetCollection<OrderFulFilledVM>()).Returns(_moqMongoCollection.Object);

            await orderRepo.CreateOrderFulFilledVM(order);

            _moqMongoCollection.Verify(mock => mock.InsertOneAsync(order, null, default), Times.Once());            
        }

        #endregion

    } 
}
