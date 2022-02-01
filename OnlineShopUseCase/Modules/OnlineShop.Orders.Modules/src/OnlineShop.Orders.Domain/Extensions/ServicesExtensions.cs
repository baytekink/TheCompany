using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Orders.Domain.Mappings;
using OnlineShop.Orders.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TheCompany.Domain.Shared.Common.Helper;
using MassTransit;
using OnlineShop.Orders.Domain.QueueMessaging; 
using OnlineShop.Products.Domain.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using Microsoft.Extensions.Configuration;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using OnlineShop.Orders.EntityFrameworkCore.Extensions;

namespace OnlineShop.Orders.Domain.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProducer<OrderCreatedObject>, OrderProducer>(); 
            services.AddAutoMapper(typeof(MappingEntitiesProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IIdGenerator, IdGenerator>();
            services.AddTransient<IDateCreator, DateCreatorUtc>();
            services.AddMassTransit(configure =>
            {
                //add consumers
                configure.AddConsumer<CustomerChangedEventConsumer>();
                configure.AddConsumer<ProductChangedEventConsumer>();
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration.GetConnectionString("RabbitMQ"));

                    configurator.ReceiveEndpoint(QueueMessagingSettings.CustomerChangedEventQueue, e => e.ConfigureConsumer<CustomerChangedEventConsumer>(context));
                    configurator.ReceiveEndpoint(QueueMessagingSettings.ProductChangedEventQueue, e => e.ConfigureConsumer<ProductChangedEventConsumer>(context));
                    configurator.ReceiveEndpoint(QueueMessagingOrderSettings.OrderCreatedEventQueue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
                });
            });
            services.AddMassTransitHostedService();
            services.AddRepositories();
            return services;
        }
    }
}
