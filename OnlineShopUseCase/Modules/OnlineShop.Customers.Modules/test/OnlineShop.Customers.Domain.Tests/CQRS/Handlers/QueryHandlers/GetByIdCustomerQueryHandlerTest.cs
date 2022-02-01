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
using AutoMapper;
using OnlineShop.Customers.Domain.Entity.Entities;
using Moq;
using OnlineShop.Customers.Domain.Mappings;
using Xunit;
using System.Linq.Expressions;

namespace OnlineShop.Customers.Domain.Handlers.QueryHandlers
{
    public class GetByIdCustomerQueryHandlerTest
    {
        private readonly Mock<ICustomerRepository<Customer>> _mockRepo;
        private readonly GetByIdCustomerQueryHandler handler;
        private readonly IMapper mapper;

        public GetByIdCustomerQueryHandlerTest()
        {
            _mockRepo = new Mock<ICustomerRepository<Customer>>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new GetByIdCustomerQueryHandler(_mockRepo.Object, mapper);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdCustomerQueryRequest()
            {   
                 Id = id,
            };

            var response = new Customer()
            {
                Id = id,
                Name = "Kemal",
                Surname = "Deniz",
                Address = "first address",
                BirthDate = new System.DateTime(2000, 1, 1),
                Gender = Shared.Enums.CustomerGender.Male,
                Phone = "123456789"
            };

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult(response )); 

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdCustomerQueryRequest()
            {
                Id = id,
            };

            Customer response = null;

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert   
            //Assert   
            Assert.Null(result);
        }
        #endregion
    }
}
