using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class ProductRepositoryTest
    {
        readonly RepositoryDbContext repositoryContext;
        public ProductRepositoryTest()
        {
            repositoryContext = new UtilityDbContext().CreateGetDatabaseContext();
        }

        #region Create
        [Fact]
        public async Task Create_ActionExecutes_ReturnsSuccess()
        {
            var repo = new ProductRepository(repositoryContext);
            var r = await repo.CreateAsync(new Domain.Entity.Entities.Product()
            {
                CreateTime = DateTime.Now,
                CreateUserId = null,
                Id = Guid.NewGuid(),
                IsDeleted = 0,
                ModifyUserId = null,
                UpdateTime = DateTime.Now,
                Brand = "Apple",
                Description = "cool phone",
                Model = "Iphone",
                Title = "Apple iphone"
            });

            Assert.Equal(1, r);
        }

        #endregion
    }
}
