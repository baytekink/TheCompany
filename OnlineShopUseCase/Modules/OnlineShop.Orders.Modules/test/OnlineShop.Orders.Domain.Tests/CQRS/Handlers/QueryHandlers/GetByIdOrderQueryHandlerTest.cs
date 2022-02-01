using OnlineShop.Orders.Domain.Queries.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using AutoMapper;
using OnlineShop.Orders.Domain.Entity.Entities;
using Moq;
using OnlineShop.Orders.Domain.Mappings;
using Xunit;
using System.Linq.Expressions;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Domain.Handlers.QueryHandlers
{
    public class GetByIdOrderQueryHandlerTest
    {
        private readonly Mock<IOrderNoSqlRepository<OrderFulFilledVM>> _mockRepo;
        private readonly GetByIdOrderQueryHandler handler;

        public GetByIdOrderQueryHandlerTest()
        {
            _mockRepo = new Mock<IOrderNoSqlRepository<OrderFulFilledVM>>();

            handler = new GetByIdOrderQueryHandler(_mockRepo.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetByIdOrderQueryRequest()
            {
                Id = id,
            };

            var response = new OrderFulFilledVM()
            {
                Id = id,
                CreateTime = DateTime.Now,
                Customer = new CustomerVM()
                {
                    Address = "The Address",
                    Name = "Kemal",
                    Phone = "11231231",
                    Surname = "Bey"
                },
                CustomerId = Guid.NewGuid(),
                OrderItems = new List<OrderItemVM>
                {
                    new OrderItemVM(){
                        Count = 1,
                        Price = 100,
                        Product = new ProductVM(){
                        Brand = "Apple",
                        Model = "Iphone",
                        Title = "Apple iphone"
                       },
                        ProductId = Guid.NewGuid(),
                    }
                },
                OrderStatus = Shared.Enums.OrderStatus.Suspend,
                TotalPrice = 100
            };

            _mockRepo.Setup(repo => repo.FindOneOrderFullFilledAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        { 
            GetByIdOrderQueryRequest request = null;

            OrderFulFilledVM response = null;

            _mockRepo.Setup(repo => repo.FindOneOrderFullFilledAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert   
            //Assert   
            Assert.Null(result);
        }
        #endregion
    }
}
