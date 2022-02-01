using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using TheCompany.EntityFrameworkCore.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Orders.Repository.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository<Customer>
    {
        public CustomerRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
