using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Products.Domain.Mappings;
using OnlineShop.Products.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TheCompany.Domain.Shared.Common.Helper;
using MassTransit;  
using TheCompany.Domain.Shared.Common.QueueMessaging;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using OnlineShop.Products.Domain.QueueMessaging;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Products.Domain.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProducer<ProductChangedObject>, ProductProducer>();

            services.AddAutoMapper(typeof(MappingEntitiesProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IIdGenerator, IdGenerator>();
            services.AddTransient<IDateCreator, DateCreatorUtc>();

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration.GetConnectionString("RabbitMQ"));
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
