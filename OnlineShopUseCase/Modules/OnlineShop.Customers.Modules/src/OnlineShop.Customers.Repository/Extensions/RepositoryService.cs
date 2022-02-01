using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Customers.Domain.Shared.Repository;
using OnlineShop.Customers.EntityFrameworkCore;
using OnlineShop.Customers.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Customers.EntityFrameworkCore.Extensions;
using OnlineShop.Customers.Domain.Entity.Entities;

namespace OnlineShop.Customers.EntityFrameworkCore.Extensions
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //add db service also
            services.AddDBContext();

            //add repositories of entities
            services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();

            return services;
        }
    }
}
