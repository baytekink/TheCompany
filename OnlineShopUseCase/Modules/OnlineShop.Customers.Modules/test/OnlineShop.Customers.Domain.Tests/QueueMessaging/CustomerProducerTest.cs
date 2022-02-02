using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Repository;
using OnlineShop.Customers.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Customers.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Customers.Domain.Commands.Request;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using OnlineShop.Customers.Domain.QueueMessaging;
using MassTransit;
using System;

namespace OnlineShop.Customers.Domain.Handlers.QueueMessaging
{
    public class CustomerProducerTest
    {
        private readonly Mock<ISendEndpointProvider> _mockProvider;
        private readonly Mock<ISendEndpoint> _mockProviderEndPoint;
        private readonly CustomerProducer producer;

        public CustomerProducerTest()
        {
            _mockProvider = new Mock<ISendEndpointProvider>();
            _mockProviderEndPoint = new Mock<ISendEndpoint>();
            producer = new CustomerProducer(_mockProvider.Object);
        }

        #region SendAsync
        [Fact]
        public async Task SendAsync_ActionExecutes_ReturnsSuccess()
        {
            var producedObj = new CustomerChangedObject()
            {
                Id = Guid.NewGuid(),
                Name = "Kemal",
                Surname = "Bey",
                Address = "The Address",
                Phone = "11231311"
            };

            var cancelToken = new System.Threading.CancellationToken();
            _mockProviderEndPoint.Setup(p => p.Send(producedObj, cancelToken));
            _mockProvider.Setup(repo => repo.GetSendEndpoint(new($"queue:{QueueMessagingSettings.CustomerChangedEventQueue}"))).Returns(Task.FromResult(_mockProviderEndPoint.Object));

            await producer.SendAsync(producedObj);

            _mockProviderEndPoint.Verify(mock => mock.Send(producedObj, cancelToken), Times.Once());
        }

        #endregion
    }
}
