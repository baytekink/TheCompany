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
using System.Collections.Generic; 
using OnlineShop.Customers.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Customers.Domain.Extensions
{
    public class ServicesExtensionsTest
    {
        private readonly ServiceCollection services;
        private readonly Mock<IConfiguration> _mockConfigurator;
        private readonly Mock<IConfigurationSection> mockConfSection;

        public ServicesExtensionsTest()
        {
            services = new ServiceCollection();
            _mockConfigurator = new Mock<IConfiguration>();
            mockConfSection = new Mock<IConfigurationSection>();
        }

        #region SendAsync
        [Fact]
        public void AddDomainServices_ReturnsSuccess()
        {
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "RabbitMQ")]).Returns("RabbitMQConnectionString");

            _mockConfigurator.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            ServicesExtensions.AddDomainServices(services, _mockConfigurator.Object);

            var provider = services.BuildServiceProvider();
            var orderProducerService = provider.GetRequiredService(typeof(IProducer<CustomerChangedObject>));
            var orderProducer = orderProducerService as CustomerProducer;
            Assert.NotNull(orderProducer);
        }

        #endregion
    }
}
