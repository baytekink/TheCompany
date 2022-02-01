using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class CustomerRepositoryTest
    {
        readonly RepositoryDbContext repositoryContext;
        public CustomerRepositoryTest()
        {
            repositoryContext = new UtilityDbContext().CreateGetDatabaseContext();
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var repo = new CustomerRepository(repositoryContext);
            var r = await repo.CreateAsync(new Domain.Entity.Entities.Customer()
            {
                Address = "the address",
                CreateTime = DateTime.Now,    
                CreateUserId = null,
                Id = Guid.NewGuid(),
                IsDeleted = 0,
                ModifyUserId = null,
                Name = "Kemal",
                Phone = "123131",
                Surname = "Bey",
                UpdateTime = DateTime.Now
            });

            Assert.Equal(1, r);
        }

        #endregion
    }
}
