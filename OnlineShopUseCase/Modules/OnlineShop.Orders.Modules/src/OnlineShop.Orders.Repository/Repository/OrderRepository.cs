using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using TheCompany.EntityFrameworkCore.Common.Repository; 

namespace OnlineShop.Orders.Repository.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository<Order>
    { 
        public OrderRepository(RepositoryDbContext repositoryContext ) : base(repositoryContext)
        { 
        } 
    }
}
