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
using OnlineShop.Products.Domain.Entity.Entities;
using AutoMapper;
using Moq;
using Xunit;
using OnlineShop.Products.Domain.Mappings;

namespace OnlineShop.Products.Domain.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandlerTest
    {
        private readonly Mock<IProductRepository<Product>> _mockRepo;
        private readonly GetAllProductQueryHandler handler;
        private readonly IMapper mapper;

        public GetAllProductQueryHandlerTest()
        {
            _mockRepo = new Mock<IProductRepository<Product>>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new GetAllProductQueryHandler(_mockRepo.Object, mapper);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetAllProductQueryRequest()
            {
            };

            var response = new List<Product>()
            {
                new Product ()
                {
                    Id = id,
                    Title = " ",
                    Brand = " ",
                    Cost = 10,
                    CreateTime = DateTime.Now,
                    Description = " ",
                    Model = " ",
                    Price = 15,
                }
            };

            _mockRepo.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<Product>)response));

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
            var request = new GetAllProductQueryRequest()
            {
            };

            var response = new List<Product>()
            { 
            };

            _mockRepo.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult((IReadOnlyList<Product>)response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Count, result.Count); 
        }
        #endregion
    }
}
