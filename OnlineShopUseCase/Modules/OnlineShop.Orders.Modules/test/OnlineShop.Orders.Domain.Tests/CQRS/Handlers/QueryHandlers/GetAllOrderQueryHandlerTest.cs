using OnlineShop.Orders.Domain.Queries.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Entity.Entities;
using AutoMapper;
using Moq;
using Xunit;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Domain.Handlers.QueryHandlers
{
    public class GetAllOrderQueryHandlerTest
    {
        private readonly Mock<IOrderNoSqlRepository<OrderFulFilledVM>> _mockRepo;
        private readonly GetAllOrderQueryHandler handler;

        public GetAllOrderQueryHandlerTest()
        {
            _mockRepo = new Mock<IOrderNoSqlRepository<OrderFulFilledVM>>();

            handler = new GetAllOrderQueryHandler(_mockRepo.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var id = Guid.NewGuid();

            var request = new GetAllOrderQueryRequest()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };

            var response = new List<OrderFulFilledVM>()
            {
                new OrderFulFilledVM ()
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
                }
            };

            _mockRepo.Setup(repo => repo.FindOrderFullFilledAsync(request.StartDate, request.EndDate)).Returns(Task.FromResult((IReadOnlyList<OrderFulFilledVM>)response));

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
            GetAllOrderQueryRequest request = null;

            var response = new List<OrderFulFilledVM>()
            {
            };

            _mockRepo.Setup(repo => repo.FindOrderFullFilledAsync(DateTime.Now, DateTime.Now)).Returns(Task.FromResult((IReadOnlyList<OrderFulFilledVM>)response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.Equal(response.Count, result.Count);
        }
        #endregion
    }
}
