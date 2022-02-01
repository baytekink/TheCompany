using OnlineShop.Products.Domain.Entity.Entities;
using OnlineShop.Products.Domain.Shared.Repository;
using OnlineShop.Products.EntityFrameworkCore;
using TheCompany.EntityFrameworkCore.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Products.Repository.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository<Product>
    {
        public ProductRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
