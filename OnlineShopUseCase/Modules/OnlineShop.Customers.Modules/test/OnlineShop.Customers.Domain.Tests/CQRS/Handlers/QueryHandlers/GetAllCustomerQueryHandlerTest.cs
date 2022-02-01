using OnlineShop.Customers.Domain.Queries.Request;
using OnlineShop.Customers.Domain.Queries.Response;
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
using Moq;
using Xunit;
using OnlineShop.Customers.Domain.Mappings;

namespace OnlineShop.Customers.Domain.Handlers.QueryHandlers
{
    public class GetAllCustomerQueryHandlerTest
    {
        private readonly Mock<ICustomerRepository<Customer>> _mockRepo;
        private readonly GetAllCustomerQueryHandler handler;
        private readonly IMapper mapper;

        public GetAllCustomerQueryHandlerTest()
        {
            _mockRepo = new Mock<ICustomerRepository<Customer>>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new GetAllCustomerQueryHandler(_mockRepo.Object, mapper);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetAllCustomerQueryRequest()
            {
            };

            var response = new List<Customer>()
            {
                new Customer ()
                {
                    Id = id,
                    Name = "Kemal",
                    Surname = "Deniz",
                    Address = "first address",
                    BirthDate = new System.DateTime(2000, 1, 1),
                    Gender = Shared.Enums.CustomerGender.Male,
                    Phone = "123456789"
                }
            };

            _mockRepo.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<Customer>)response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Count, result.Count);
            if (response.Count > 0)
                Assert.Equal(response[0].Id, result[0].Id);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsEmpty()
        {
            var request = new GetAllCustomerQueryRequest()
            {
            };

            var response = new List<Customer>()
            {
            };

            _mockRepo.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<Customer>)response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Count, result.Count);
        }
        #endregion
    }
}
