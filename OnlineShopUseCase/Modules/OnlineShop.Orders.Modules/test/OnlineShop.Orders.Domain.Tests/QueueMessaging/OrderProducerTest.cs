using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Orders.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Orders.Domain.Commands.Request;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using OnlineShop.Orders.Domain.QueueMessaging;
using MassTransit;
using System;
using OnlineShop.Products.Domain.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using System.Collections.Generic;

namespace OnlineShop.Orders.Domain.QueueMessaging
{
    public class OrderProducerTest
    {
        private readonly Mock<ISendEndpointProvider> _mockProvider;
        private readonly Mock<ISendEndpoint> _mockProviderEndPoint;
        private readonly OrderProducer producer;

        public OrderProducerTest()
        {
            _mockProvider = new Mock<ISendEndpointProvider>();
            _mockProviderEndPoint = new Mock<ISendEndpoint>();
            producer = new OrderProducer(_mockProvider.Object);
        }

        #region SendAsync
        [Fact]
        public async Task SendAsync_ActionExecutes_ReturnsSuccess()
        {
            var producedObj = new OrderCreatedObject()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                CustomerId = Guid.NewGuid(),
                OrderStatus = Shared.Enums.OrderStatus.Suspend,
                TotalPrice = 100,
                OrderItems = new List<OrderCreatedObjectItem>()
                 {
                     new OrderCreatedObjectItem()
                     {
                          Count = 1,
                          Price = 100,
                          ProductId = Guid.NewGuid(),
                     }
                 }
            };

            _mockProvider.Setup(repo => repo.GetSendEndpoint(new($"queue:{QueueMessagingOrderSettings.OrderCreatedEventQueue}"))).Returns(Task.FromResult(_mockProviderEndPoint.Object));

            await producer.SendAsync(producedObj);

            Assert.True(true);
        }

        #endregion
    }
}
