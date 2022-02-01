using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineShop.Orders.HttpApi.Controllers;
using MediatR;
using OnlineShop.Orders.Domain.Commands.Request;
using OnlineShop.Orders.Domain.Commands.Response;
using System.Threading;
using OnlineShop.Orders.Domain.Queries.Request;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.HttpApi.Tests
{
    public class OrderControllerTest
    {
        private readonly Mock<IMediator> _mockRepo;
        private readonly OrderController _controller;
        private readonly IIdGenerator idGenerator;

        public OrderControllerTest()
        {
            idGenerator = new IdGenerator();
            _mockRepo = new Mock<IMediator>();
            _controller = new OrderController(_mockRepo.Object);
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
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

            var response = new CreateOrderCommandResponse
            {
                IsSuccess = true
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as CreateOrderCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Create_ActionExecutes_ReturnsError()
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

            var response = new CreateOrderCommandResponse
            {
                IsSuccess = false
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as CreateOrderCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdOrderQueryRequest()
            {
                Id = id
            };

            var response = new OrderFulFilledVM()
            {
                Id = id,
                CustomerId = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                TotalPrice = 10,
                OrderStatus = Domain.Shared.Enums.OrderStatus.Completed,
                OrderItems = new List<OrderItemVM>()
                {
                    new OrderItemVM(){
                        Price = 10,
                        Count=1,
                        ProductId = Guid .NewGuid()
                    }
                },
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Get(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var obj = okObjectResult?.Value as OrderFulFilledVM;
            Assert.NotNull(obj);
            Assert.Equal(id, obj?.Id);
        }

        [Fact]
        public async Task Get_ActionExecutes_ReturnsError()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdOrderQueryRequest()
            {
                Id = id
            };

            OrderFulFilledVM? response = null;

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Get(request);

            //Assert 
            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult?.StatusCode);
        }
        #endregion

        #region GetAll
        [Fact]
        public async Task GetAll_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetAllOrderQueryRequest()
            {
            };

            var response = new List<OrderFulFilledVM>
            {
                new OrderFulFilledVM()
                {
                    Id = id,
                    CustomerId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    TotalPrice = 10,
                    OrderStatus = Domain.Shared.Enums.OrderStatus.Completed,
                    OrderItems = new List< OrderItemVM>()
                    {
                        new OrderItemVM(){
                            Price = 10,
                            Count=1,
                            ProductId = Guid .NewGuid()
                        }
                    },
                }
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<OrderFulFilledVM>)response));

            var result = await _controller.GetAll(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<OrderFulFilledVM>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
            if (response.Count > 0 && objList?.Count > 0)
                Assert.Equal(response[0].Id, objList[0].Id);
        }

        [Fact]
        public async Task GetAll_ActionExecutes_ReturnsEmpty()
        {
            var request = new GetAllOrderQueryRequest()
            {
            };

            var response = new List<OrderFulFilledVM>();

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<OrderFulFilledVM>)response));

            var result = await _controller.GetAll(request);


            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<OrderFulFilledVM>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
        }
        #endregion
    }
}
