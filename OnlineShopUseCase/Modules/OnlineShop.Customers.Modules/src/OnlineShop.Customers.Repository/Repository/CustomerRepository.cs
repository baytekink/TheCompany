using OnlineShop.Customers.Domain.Entity.Entities;
using OnlineShop.Customers.Domain.Shared.Repository;
using OnlineShop.Customers.EntityFrameworkCore;
using TheCompany.EntityFrameworkCore.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Customers.Repository.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository<Customer>
    {
        public CustomerRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
