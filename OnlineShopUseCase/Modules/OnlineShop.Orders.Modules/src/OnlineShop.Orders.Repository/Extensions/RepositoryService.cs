using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using OnlineShop.Orders.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Orders.EntityFrameworkCore.Extensions;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.MongoDB;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.EntityFrameworkCore.Extensions
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //add db service also
            services.AddDBContext();            
            services.AddSingleton<INoSqlService, MongoDbService>();

            //add repositories of entities
            services.AddScoped<IOrderRepository<Order>, OrderRepository>();
            services.AddScoped<IOrderNoSqlRepository<OrderFulFilledVM>, OrderRepositoryQuery>();
            services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();
            services.AddScoped<IProductRepository<Product>, ProductRepository>();            

            return services;
        }
    }
}
