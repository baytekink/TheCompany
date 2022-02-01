using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Orders.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Orders.Domain.Commands.Request;
using OnlineShop.Orders.Domain.Commands.Response;
using System.Collections.Generic;
using MassTransit;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;

namespace OnlineShop.Orders.Domain.Handlers.CommandHandlers
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly Mock<IOrderRepository<Order>> _mockRepo;
        private readonly Mock<IProducer<OrderCreatedObject>> _mockProducer;
        private readonly CreateOrderCommandHandler handler;
        private readonly IIdGenerator idGenerator;
        private readonly IMapper mapper;
        private readonly IDateCreator dateCreator;

        public CreateOrderCommandHandlerTest()
        {
            _mockRepo = new Mock<IOrderRepository<Order>>();
            _mockProducer = new Mock<IProducer<OrderCreatedObject>>();

            dateCreator = new DateCreatorUtc();
            idGenerator = new IdGenerator();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new CreateOrderCommandHandler(_mockRepo.Object, mapper, idGenerator, dateCreator, _mockProducer.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var customerId = idGenerator.GenerateId();
            var OrderItems = new List<CreateOrderItem>()
            {
                new CreateOrderItem(){
                    ProductId = idGenerator.GenerateId(),
                    Count = 1,
                    Price = 10
                }
            };

            var request = new CreateOrderCommandRequest()
            {
                CustomerId = customerId,
                OrderItems = OrderItems
            };

            var response = 1;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Order>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var customerId = idGenerator.GenerateId();
            var OrderItems = new List<CreateOrderItem>()
            {
                new CreateOrderItem(){
                    ProductId = idGenerator.GenerateId(),
                    Count = 1,
                    Price = 10
                }
            };

            var request = new CreateOrderCommandRequest()
            {
                CustomerId = customerId,
                OrderItems = OrderItems
            };

            var response = 0;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Order>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}
