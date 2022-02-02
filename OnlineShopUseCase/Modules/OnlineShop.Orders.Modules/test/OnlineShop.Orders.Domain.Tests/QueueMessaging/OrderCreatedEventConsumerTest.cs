using System.Threading.Tasks;
using Moq;
using Xunit;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using MassTransit;
using System;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.QueueMessaging;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Orders.Domain.Mappings;
using System.Linq.Expressions;
using OnlineShop.Orders.Domain.Shared.ViewModels;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using System.Collections.Generic;

namespace OnlineShop.Orders.Domain.QueueMessaging
{
    public class OrderCreatedEventConsumerTest
    {
        private readonly Mock<IOrderNoSqlRepository<OrderFulFilledVM>> _mockProvider;
        private readonly Mock<ICustomerRepository<Customer>> mockCustomerRepo;
        private readonly Mock<IProductRepository<Product>> mockProductRepo;
        private readonly Mock<ConsumeContext<OrderCreatedObject>> mockContext;

        private readonly IMapper mapper;

        private readonly OrderCreatedEventConsumer consumer;

        public OrderCreatedEventConsumerTest()
        {
            _mockProvider = new Mock<IOrderNoSqlRepository<OrderFulFilledVM>>();
            mockCustomerRepo = new Mock<ICustomerRepository<Customer>>();
            mockProductRepo = new Mock<IProductRepository<Product>>();
            mockContext = new Mock<ConsumeContext<OrderCreatedObject>>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            consumer = new OrderCreatedEventConsumer(_mockProvider.Object, mockCustomerRepo.Object, mockProductRepo.Object, mapper);
        }

        #region ConsumeAsync
        [Fact]
        public async Task ConsumeUpdateAsync_ActionExecutes_ReturnsSuccess()
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Title = "Apple iphone",
                Brand = "Apple",
                Model = "Iphone",
                CreateTime = DateTime.Now,
                CreateUserId = Guid.NewGuid(),
                Description = "The description",
                IsDeleted = 0,
                ModifyUserId = Guid.NewGuid(),
                UpdateTime = DateTime.Now,
            };

            List<Product> productsList = new();
            productsList.Add(product);

            Customer customer = new()
            {
                Name = "Kemal",
                Surname = "Bey",
                CreateTime = DateTime.Now,
                Address = "The Address",
                CreateUserId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsDeleted = 0,
                ModifyUserId = Guid.NewGuid(),
                Phone = "1231231",
                UpdateTime = DateTime.Now,
            };
  
            mockProductRepo.Setup(repo => repo.FindByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult((IReadOnlyList<Product>)productsList));

            mockCustomerRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult(customer));

            _mockProvider.Setup(repo => repo.CreateOrderFulFilledVM(It.IsAny<OrderFulFilledVM>()));

            mockContext.SetupGet(m => m.Message).Returns(new OrderCreatedObject()
            {
                Id = product.Id,
                CreateTime = DateTime.Now,
                CustomerId = customer.Id,
                TotalPrice = 100,
                OrderStatus = Orders.Domain.Shared.Enums.OrderStatus.Suspend,
                OrderItems = new List<OrderCreatedObjectItem>()
                {
                    new OrderCreatedObjectItem(){
                        Count = 1,
                        Price = 100,
                        ProductId = product.Id
                    }
                }
            });

            await consumer.Consume(mockContext.Object);

            _mockProvider.Verify(mock => mock.CreateOrderFulFilledVM(It.IsAny<OrderFulFilledVM>()), Times.Once());
        }

        #endregion
    }
}
