using System.Threading.Tasks;
using OnlineShop.Products.Domain.Shared.Repository;
using OnlineShop.Products.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Products.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Products.Domain.Commands.Request;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using OnlineShop.Products.Domain.QueueMessaging;
using MassTransit;
using System;

namespace OnlineShop.Products.Domain.Handlers.CommandHandlers
{
    public class ProductProducerTest
    {
        private readonly Mock<ISendEndpointProvider> _mockProvider;
        private readonly Mock<ISendEndpoint> _mockProviderEndPoint;
        private readonly ProductProducer producer;

        public ProductProducerTest()
        {
            _mockProvider = new Mock<ISendEndpointProvider>();
            _mockProviderEndPoint = new Mock<ISendEndpoint>();
            producer = new ProductProducer(_mockProvider.Object);
        }

        #region SendAsync
        [Fact]
        public async Task SendAsync_ActionExecutes_ReturnsSuccess()
        {
            var producedObj = new ProductChangedObject()
            {
                Id = Guid.NewGuid(),
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            _mockProvider.Setup(repo => repo.GetSendEndpoint(new($"queue:{QueueMessagingSettings.ProductChangedEventQueue}"))).Returns(Task.FromResult(_mockProviderEndPoint.Object));

            await producer.SendAsync(producedObj);

            Assert.True(true);
        }

        #endregion
    }
}
