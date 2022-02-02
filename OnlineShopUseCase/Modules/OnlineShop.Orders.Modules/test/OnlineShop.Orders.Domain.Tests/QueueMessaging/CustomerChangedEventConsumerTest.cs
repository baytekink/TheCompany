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
    public class CustomerChangedEventConsumerTest
    {
        private readonly Mock<ICustomerRepository<Customer>> _mockProvider;
        private readonly Mock<ConsumeContext<CustomerChangedObject>> mockContext;
        private readonly IMapper mapper;
        private readonly IDateCreator dateCreator;

        private readonly CustomerChangedEventConsumer consumer;

        public CustomerChangedEventConsumerTest()
        {
            _mockProvider = new Mock<ICustomerRepository<Customer>>();
            mockContext = new Mock<ConsumeContext<CustomerChangedObject>>();
            dateCreator = new DateCreatorUtc();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            consumer = new CustomerChangedEventConsumer(_mockProvider.Object, mapper, dateCreator);
        }

        #region ConsumeAsync
        [Fact]
        public async Task ConsumeUpdateAsync_ActionExecutes_ReturnsSuccess()
        {
            Customer customer = new()
            {
                Id = Guid.NewGuid(),
                Name = "Kemal",
                Surname = "Bey",
                Address = "The Address",
                Phone = "11231311"
            };

            _mockProvider.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult(customer));
            _mockProvider.Setup(repo => repo.UpdateWithSaveAsync(customer));

            mockContext.SetupGet(m => m.Message).Returns(new CustomerChangedObject()
            {
                Id = customer.Id,
                Address = customer.Address,
                Name = customer.Name,
                Phone = customer.Phone,
                Surname = customer.Surname
            });

            await consumer.Consume(mockContext.Object);

            _mockProvider.Verify(mock => mock.UpdateWithSaveAsync(customer), Times.Once());
        }

        [Fact]
        public async Task ConsumeAddAsync_ActionExecutes_ReturnsSuccess()
        {  
            _mockProvider.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult((Customer)null));
            _mockProvider.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Customer>()));

            mockContext.SetupGet(m => m.Message).Returns(new CustomerChangedObject()
            {
                Id = Guid.NewGuid(),
                Name = "Kemal",
                Surname = "Bey",
                Address = "The Address",
                Phone = "11231311"
            });

            await consumer.Consume(mockContext.Object);
             
            _mockProvider.Verify(mock => mock.CreateWithSaveAsync(It.IsAny<Customer>()), Times.Once());
        }

        #endregion
    }
}
