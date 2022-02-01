﻿using OnlineShop.Products.Domain.Commands.Request;
using OnlineShop.Products.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Products.Domain.Shared.Repository;
using OnlineShop.Products.Domain.Entity.Entities;
using TheCompany.Domain.Shared.Common.Helper;
using Moq;
using AutoMapper;
using OnlineShop.Products.Domain.Mappings;
using Xunit;
using System.Linq.Expressions;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Products.Domain.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository<Product>> _mockRepo;
        private readonly Mock<IProducer<ProductChangedObject>> _mockProducer;
        private readonly DeleteProductCommandHandler handler;
        private readonly IDateCreator dateCreator;
        private readonly IMapper mapper;

        public DeleteProductCommandHandlerTest()
        {
            _mockRepo = new Mock<IProductRepository<Product>>();
            _mockProducer = new Mock<IProducer<ProductChangedObject>>();
            dateCreator = new DateCreatorUtc();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new DeleteProductCommandHandler(_mockRepo.Object, dateCreator, mapper, _mockProducer.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new DeleteProductCommandRequest()
            {
                Id = id
            };

            Product p = new()
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

            var response = 1;

            _mockRepo.Setup(repo => repo.FindOneByConditionAsync(It.IsAny<Expression<Func<Product, bool>>>())).Returns(Task.FromResult(p));
            _mockRepo.Setup(repo => repo.UpdateWithSaveAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var request = new DeleteProductCommandRequest()
            {
                Id = Guid.NewGuid()
            };

            var response = 0;

            _mockRepo.Setup(repo => repo.UpdateWithSaveAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}
