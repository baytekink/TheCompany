using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineShop.Products.HttpApi.Controllers;
using MediatR;
using OnlineShop.Products.Domain.Commands.Request;
using OnlineShop.Products.Domain.Commands.Response;
using System.Threading;
using OnlineShop.Products.Domain.Queries.Request;
using OnlineShop.Products.Domain.Queries.Response;

namespace OnlineShop.Products.HttpApi.Tests.Controllers
{
    public class ProductControllerTest
    {
        private readonly Mock<IMediator> mockMediator;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            mockMediator = new Mock<IMediator>();
            _controller = new ProductController(mockMediator.Object);
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var request = new CreateProductCommandRequest()
            {
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = new CreateProductCommandResponse
            {
                IsSuccess = true,
                Id = Guid.NewGuid()
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as CreateProductCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Create_ActionExecutes_ReturnsError()
        {
            var request = new CreateProductCommandRequest()
            {
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = new CreateProductCommandResponse
            {
                IsSuccess = false,
                Id = Guid.NewGuid()
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as CreateProductCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_ActionExecutes_ReturnsSuccess()
        {
            var request = new UpdateProductCommandRequest()
            {
                Id = Guid.NewGuid(),
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = new UpdateProductCommandResponse
            {
                IsSuccess = true
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Update(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as UpdateProductCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Update_ActionExecutes_ReturnsError()
        {
            var request = new UpdateProductCommandRequest()
            {
                Id = Guid.NewGuid(),
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = new UpdateProductCommandResponse
            {
                IsSuccess = false
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Update(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as UpdateProductCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ActionExecutes_ReturnsSuccess()
        {
            var request = new DeleteProductCommandRequest()
            {
                Id = Guid.NewGuid(),
            };

            var response = new DeleteProductCommandResponse
            {
                IsSuccess = true
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Delete(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as DeleteProductCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Delete_ActionExecutes_ReturnsError()
        {
            var request = new DeleteProductCommandRequest()
            {
                Id = Guid.NewGuid(),
            };

            var response = new DeleteProductCommandResponse
            {
                IsSuccess = false
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Delete(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as DeleteProductCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdProductQueryRequest()
            {
                Id = id
            };

            var response = new GetByIdProductQueryResponse()
            {
                Id = id,
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsDeleted = 0
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Get(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var obj = okObjectResult?.Value as GetByIdProductQueryResponse;
            Assert.NotNull(obj);
            Assert.Equal(id, obj?.Id);
        }

        [Fact]
        public async Task Get_ActionExecutes_ReturnsError()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdProductQueryRequest()
            {
                Id = id
            };

            GetByIdProductQueryResponse? response = null;

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

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

            var request = new GetAllProductQueryRequest()
            {
            };

            var response = new List<GetAllProductQueryResponse>
            {
                new GetAllProductQueryResponse()
                {
                    Id = id,
                    Title = "Stroller",
                    Description = "Carries your baby in safe",
                    Brand = "Mima",
                    Model = "Zigi",
                    Cost = 10,
                    Price = 15,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    IsDeleted = 0
                }
            };

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<GetAllProductQueryResponse>)response));

            var result = await _controller.GetAll(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<GetAllProductQueryResponse>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
            if (response.Count > 0 && objList?.Count > 0)
                Assert.Equal(response[0].Id, objList[0].Id);
        }

        [Fact]
        public async Task GetAll_ActionExecutes_ReturnsEmpty()
        {
            var request = new GetAllProductQueryRequest()
            {
            };

            var response = new List<GetAllProductQueryResponse>();

            mockMediator.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<GetAllProductQueryResponse>)response));

            var result = await _controller.GetAll(request);


            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<GetAllProductQueryResponse>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
        }
        #endregion
    }
}
