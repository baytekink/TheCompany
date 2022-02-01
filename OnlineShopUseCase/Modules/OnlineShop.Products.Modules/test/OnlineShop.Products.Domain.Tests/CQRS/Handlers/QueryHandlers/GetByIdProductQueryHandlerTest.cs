using OnlineShop.Products.Domain.Queries.Request;
using OnlineShop.Products.Domain.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Products.Domain.Shared.Repository;
using AutoMapper;
using OnlineShop.Products.Domain.Entity.Entities;
using Moq;
using OnlineShop.Products.Domain.Mappings;
using Xunit;
using System.Linq.Expressions;

namespace OnlineShop.Products.Domain.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandlerTest
    {
        private readonly Mock<IProductRepository<Product>> _mockRepo;
        private readonly GetByIdProductQueryHandler handler;
        private readonly IMapper mapper;

        public GetByIdProductQueryHandlerTest()
        {
            _mockRepo = new Mock<IProductRepository<Product>>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new GetByIdProductQueryHandler(_mockRepo.Object, mapper);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdProductQueryRequest()
            {   
                 Id = id,
            };

            var response = new Product()
            {
                Id = id,
                Title = " ",
                Brand = " ",
                Cost = 10,
                CreateTime = DateTime.Now,
                Description = " ",
                Model = " ",
                Price = 15,
            };

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(response )); 

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdProductQueryRequest()
            {
                Id = id,
            };

            Product response = null;

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert   
            //Assert   
            Assert.Null(result);
        }
        #endregion
    }
}
