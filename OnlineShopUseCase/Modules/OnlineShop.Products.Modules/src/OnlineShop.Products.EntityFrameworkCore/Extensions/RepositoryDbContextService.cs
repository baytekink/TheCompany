using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Products.Domain.Entity.Entities; 
using OnlineShop.Products.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Products.EntityFrameworkCore.Extensions
{
    public static class RepositoryDbContextService
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services)
        {
            services.AddDbContext<DbContext, RepositoryDbContext >(options =>
            {                
            });
             
            return services;
        }
    }
}
