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

namespace OnlineShop.Orders.Domain.QueueMessaging
{
    public class ProductChangedEventConsumerTest
    {
        private readonly Mock<IProductRepository<Product>> _mockProvider;
        private readonly Mock<ConsumeContext<ProductChangedObject>> mockContext;
        private readonly IMapper mapper;
        private readonly IDateCreator dateCreator;

        private readonly ProductChangedEventConsumer consumer;

        public ProductChangedEventConsumerTest()
        {
            _mockProvider = new Mock<IProductRepository<Product>>();
            mockContext = new Mock<ConsumeContext<ProductChangedObject>>();
            dateCreator = new DateCreatorUtc();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            consumer = new ProductChangedEventConsumer(_mockProvider.Object, mapper, dateCreator);
        }

        #region ConsumeAsync
        [Fact]
        public async Task ConsumeUpdateAsync_ActionExecutes_ReturnsSuccess()
        {
            Product Product = new()
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

            _mockProvider.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(Product));
            _mockProvider.Setup(repo => repo.UpdateWithSaveAsync(Product));

            mockContext.SetupGet(m => m.Message).Returns(new ProductChangedObject()
            {
                Id = Product.Id,
                Title = "Apple iphone",
                Brand = "Apple",
                Model = "Iphone",
                Description = "The description",
                Cost = 10,
                Price = 100
            });

            await consumer.Consume(mockContext.Object);

            Assert.True(true);
        }

        [Fact]
        public async Task ConsumeAddAsync_ActionExecutes_ReturnsSuccess()
        {
            Product cNull = null;

            Product Product = new()
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

            _mockProvider.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(cNull));
            _mockProvider.Setup(repo => repo.CreateWithSaveAsync(Product));

            mockContext.SetupGet(m => m.Message).Returns(new ProductChangedObject()
            {
                Id = Product.Id,
                Title = "Apple iphone",
                Brand = "Apple",
                Model = "Iphone",
                Description = "The description",
                Cost = 10,
                Price = 100
            });

            await consumer.Consume(mockContext.Object);

            Assert.True(true);
        }

        #endregion
    }
}
