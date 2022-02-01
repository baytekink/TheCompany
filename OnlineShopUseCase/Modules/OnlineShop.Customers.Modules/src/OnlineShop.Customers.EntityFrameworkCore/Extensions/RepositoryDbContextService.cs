using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Customers.Domain.Entity.Entities;
using OnlineShop.Customers.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Customers.EntityFrameworkCore.Extensions
{
    public static class RepositoryDbContextService
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services)
        {
            services.AddDbContext<DbContext, RepositoryDbContext>();

            return services;
        }
    }
}
