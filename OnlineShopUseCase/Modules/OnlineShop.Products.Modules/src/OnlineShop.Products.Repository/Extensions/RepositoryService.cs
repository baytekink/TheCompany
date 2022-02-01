using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Products.Domain.Shared.Repository;
using OnlineShop.Products.EntityFrameworkCore;
using OnlineShop.Products.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Products.EntityFrameworkCore.Extensions;
using OnlineShop.Products.Domain.Entity.Entities;

namespace OnlineShop.Products.EntityFrameworkCore.Extensions
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //add db service also
            services.AddDBContext();

            //add repositories of entities
            services.AddScoped<IProductRepository<Product>, ProductRepository>();

            return services;
        }
    }
}
