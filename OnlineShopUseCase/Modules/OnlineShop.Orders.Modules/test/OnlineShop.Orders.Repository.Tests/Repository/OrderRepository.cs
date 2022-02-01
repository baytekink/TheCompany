using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using OnlineShop.Orders.Domain.Entity.Entities;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class OrderRepositoryTest
    {
        readonly RepositoryDbContext repositoryContext;
        public OrderRepositoryTest()
        {
            repositoryContext = new UtilityDbContext().CreateGetDatabaseContext();
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var repo = new OrderRepository(repositoryContext);
            var r = await repo.CreateAsync(new Domain.Entity.Entities.Order()
            {
                CreateTime = DateTime.Now,
                CreateUserId = null,
                Id = Guid.NewGuid(),
                IsDeleted = 0,
                ModifyUserId = null,
                UpdateTime = DateTime.Now,
                CustomerId = Guid.NewGuid(),
                OrderStatus = Domain.Shared.Enums.OrderStatus.Suspend,
                TotalPrice = 100,
                OrderItems = new OrderItem[] {
                    new OrderItem(){
                        Count=1,
                        CreateTime = DateTime.Now,
                        CreateUserId = Guid.NewGuid(),
                        IsDeleted = 0,
                        ModifyUserId = Guid.NewGuid(),
                        OrderId = Guid.NewGuid(),
                        Price = 100,
                        ProductId= Guid.NewGuid(),
                        UpdateTime = DateTime.Now,
                    }
                }
            });

            Assert.Equal(1, r);
        }

        #endregion
    }
}
