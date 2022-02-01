using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineShop.Customers.HttpApi.Controllers;
using MediatR;
using OnlineShop.Customers.Domain.Commands.Request;
using OnlineShop.Customers.Domain.Commands.Response;
using System.Threading;
using OnlineShop.Customers.Domain.Queries.Request;
using OnlineShop.Customers.Domain.Queries.Response;

namespace OnlineShop.Customers.HttpApi.Tests
{
    public class CustomerControllerTest
    {
        private readonly Mock<IMediator> _mockRepo;
        private readonly CustomerController _controller;

        public CustomerControllerTest()
        {
            _mockRepo = new Mock<IMediator>();
            _controller = new CustomerController(_mockRepo.Object);
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var request = new CreateCustomerCommandRequest()
            {
                Name = "Kemal",
                Surname = "Ay",
                Address = "The first address",
                BirthDate = new DateTime(2000, 1, 1),
                Gender = Domain.Shared.Enums.CustomerGender.Male,
                Phone = "123"
            };

            var response = new CreateCustomerCommandResponse
            {
                IsSuccess = true,
                Id = Guid.NewGuid()
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as CreateCustomerCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Create_ActionExecutes_ReturnsError()
        {
            var request = new CreateCustomerCommandRequest()
            {
                Name = "Kemal",
                Surname = "Ay",
                Address = "The first address",
                BirthDate = new DateTime(2000, 1, 1),
                Gender = Domain.Shared.Enums.CustomerGender.Male,
                Phone = "123"
            };

            var response = new CreateCustomerCommandResponse
            {
                IsSuccess = false,
                Id = Guid.NewGuid()
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Create(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as CreateCustomerCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_ActionExecutes_ReturnsSuccess()
        {
            var request = new UpdateCustomerCommandRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Kemal",
                Surname = "Ay",
                Address = "The first address",
                BirthDate = new DateTime(2000, 1, 1),
                Gender = Domain.Shared.Enums.CustomerGender.Male,
                Phone = "1234"
            };

            var response = new UpdateCustomerCommandResponse
            {
                IsSuccess = true
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Update(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as UpdateCustomerCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Update_ActionExecutes_ReturnsError()
        {
            var request = new UpdateCustomerCommandRequest()
            {
                Id = Guid.NewGuid(),
                Name = "Kemal",
                Surname = "Ay",
                Address = "The first address",
                BirthDate = new DateTime(2000, 1, 1),
                Gender = Domain.Shared.Enums.CustomerGender.Male,
                Phone = "1234"
            };

            var response = new UpdateCustomerCommandResponse
            {
                IsSuccess = false
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Update(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as UpdateCustomerCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ActionExecutes_ReturnsSuccess()
        {
            var request = new DeleteCustomerCommandRequest()
            {
                Id = Guid.NewGuid(),
            };

            var response = new DeleteCustomerCommandResponse
            {
                IsSuccess = true
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Delete(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.True((okObjectResult?.Value as DeleteCustomerCommandResponse)?.IsSuccess);
        }

        [Fact]
        public async Task Delete_ActionExecutes_ReturnsError()
        {
            var request = new DeleteCustomerCommandRequest()
            {
                Id = Guid.NewGuid(),
            };

            var response = new DeleteCustomerCommandResponse
            {
                IsSuccess = false
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Delete(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);
            Assert.False((okObjectResult?.Value as DeleteCustomerCommandResponse)?.IsSuccess);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdCustomerQueryRequest()
            {
                Id = id
            };

            var response = new GetByIdCustomerQueryResponse()
            {
                Id = id,
                Name = "Kemal",
                Surname = "Ay",
                Address = "The first address",
                BirthDate = new DateTime(2000, 1, 1),
                Gender = Domain.Shared.Enums.CustomerGender.Male,
                Phone = "1234",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsDeleted = 0
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult(response));

            var result = await _controller.Get(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var obj = okObjectResult?.Value as GetByIdCustomerQueryResponse;
            Assert.NotNull(obj);
            Assert.Equal(id, obj?.Id);
        }

        [Fact]
        public async Task Get_ActionExecutes_ReturnsError()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdCustomerQueryRequest()
            {
                Id = id
            };

            GetByIdCustomerQueryResponse? response = null;

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

            var request = new GetAllCustomerQueryRequest()
            {
            };

            var response = new List<GetAllCustomerQueryResponse>
            {
                new GetAllCustomerQueryResponse()
                {
                    Id = id,
                    Name = "Kemal",
                    Surname = "Ay",
                    Address = "The first address",
                    BirthDate = new DateTime(2000, 1, 1),
                    Gender = Domain.Shared.Enums.CustomerGender.Male,
                    Phone = "1234",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    IsDeleted = 0
                }
            };

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<GetAllCustomerQueryResponse>)response));

            var result = await _controller.GetAll(request);

            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<GetAllCustomerQueryResponse>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
            if (response.Count > 0 && objList?.Count > 0)
                Assert.Equal(response[0].Id, objList[0].Id);
        }

        [Fact]
        public async Task GetAll_ActionExecutes_ReturnsEmpty()
        {
            var request = new GetAllCustomerQueryRequest()
            {
            };

            var response = new List<GetAllCustomerQueryResponse>();

            _mockRepo.Setup(repo => repo.Send(request, default)).Returns(Task.FromResult((IReadOnlyList<GetAllCustomerQueryResponse>)response));

            var result = await _controller.GetAll(request);


            //Assert 
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.Equal(200, okObjectResult?.StatusCode);

            var objList = okObjectResult?.Value as IReadOnlyList<GetAllCustomerQueryResponse>;
            Assert.NotNull(objList);
            Assert.Equal(response.Count, objList?.Count);
        }
        #endregion
    }
}
