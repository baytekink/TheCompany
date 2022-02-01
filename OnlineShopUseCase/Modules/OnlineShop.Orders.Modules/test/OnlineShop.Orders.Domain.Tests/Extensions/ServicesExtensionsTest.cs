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
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Orders.Domain.Extensions;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Orders.Domain.Extensions
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
            var orderProducerService = provider.GetRequiredService(typeof(IProducer<OrderCreatedObject>));
            var orderProducer = orderProducerService as OrderProducer;
            Assert.NotNull(orderProducer);
        }

        #endregion
    }
}
