using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Repository;
using OnlineShop.Customers.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Customers.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Customers.Domain.Commands.Request;  
using TheCompany.Domain.Shared.Common.QueueMessaging;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Customers.Domain.Handlers.CommandHandlers
{
    public class CreateCustomerCommandHandlerTest
    {
        private readonly Mock<ICustomerRepository<Customer>> _mockRepo;
        private readonly Mock<IProducer<CustomerChangedObject>> _mockProducer;
        private readonly CreateCustomerCommandHandler handler;
        private readonly IIdGenerator idGenerator;
        private readonly IMapper mapper;
        private readonly IDateCreator dateCreator;

        public CreateCustomerCommandHandlerTest()
        {
            _mockRepo = new Mock<ICustomerRepository<Customer>>();

            dateCreator = new DateCreatorUtc();            
            idGenerator = new IdGenerator();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            _mockProducer = new Mock<IProducer<CustomerChangedObject>>();

            handler = new CreateCustomerCommandHandler(_mockRepo.Object, mapper, idGenerator, dateCreator, _mockProducer.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var request = new CreateCustomerCommandRequest()
            {
                Name = "Kemal",
                Surname = "Deniz",
                Address = "first address",
                BirthDate = new System.DateTime(2000, 1, 1),
                Gender = Shared.Enums.CustomerGender.Male,
                Phone = "123456789"
            };

            var response = 1;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Customer>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var request = new CreateCustomerCommandRequest()
            {
                Name = "Kemal",
                Surname = "Deniz",
                Address = "first address",
                BirthDate = new System.DateTime(2000, 1, 1),
                Gender = Shared.Enums.CustomerGender.Male,
                Phone = "123456789"
            };

            var response = 0;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Customer>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}
