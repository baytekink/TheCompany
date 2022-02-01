using OnlineShop.Customers.Domain.Commands.Request;
using OnlineShop.Customers.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Repository;
using OnlineShop.Customers.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using Moq;
using OnlineShop.Customers.Domain.Mappings;
using Xunit;
using System.Linq.Expressions; 
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Customers.Domain.Handlers.CommandHandlers
{
    public class UpdateCustomerCommandHandlerTest
    {
        private readonly Mock<ICustomerRepository<Customer>> _mockRepo;
        private readonly Mock<IProducer<CustomerChangedObject>> _mockProducer;
        private readonly UpdateCustomerCommandHandler handler;
        private readonly IDateCreator dateCreator;
        private readonly IMapper mapper;

        public UpdateCustomerCommandHandlerTest()
        {
            _mockRepo = new Mock<ICustomerRepository<Customer>>();

            dateCreator = new DateCreatorUtc();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();
             
            _mockProducer = new Mock<IProducer<CustomerChangedObject>>();
            handler = new UpdateCustomerCommandHandler(_mockRepo.Object, mapper, dateCreator, _mockProducer.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new UpdateCustomerCommandRequest()
            {
                Id = id,
                Name = "Kemal",
                Surname = "Deniz",
                Address = "first address",
                BirthDate = new System.DateTime(2000, 1, 1),
                Gender = Shared.Enums.CustomerGender.Male,
                Phone = "123456789"
            };

            Customer p = new()
            {
                Id = id,
                Name = "Kemal",
                Surname = "Deniz",
                Address = "first address",
                BirthDate = new System.DateTime(2000, 1, 1),
                Gender = Shared.Enums.CustomerGender.Male,
                Phone = "123456789"
            };

            var response = 1;

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult(p));
            _mockRepo.Setup(repo => repo.UpdateWithSaveAsync(It.IsAny<Customer>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var request = new UpdateCustomerCommandRequest()
            {
                Id = Guid.NewGuid()
            };

            var response = 0;

            _mockRepo.Setup(repo => repo.UpdateWithSaveAsync(It.IsAny<Customer>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}
