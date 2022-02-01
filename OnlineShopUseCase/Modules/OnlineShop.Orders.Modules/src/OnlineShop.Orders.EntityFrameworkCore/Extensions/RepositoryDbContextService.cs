using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Orders.EntityFrameworkCore.Extensions
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
