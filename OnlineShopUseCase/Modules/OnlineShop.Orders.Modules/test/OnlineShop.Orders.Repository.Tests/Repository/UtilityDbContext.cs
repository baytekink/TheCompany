using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using OnlineShop.Orders.Repository.Repository;
using OnlineShop.Orders.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OnlineShop.Orders.Repository.Tests.Repository
{
    public class UtilityDbContext
    {
        private readonly Mock<IConfigurationSection> mockConfSection;
        public UtilityDbContext()
        {
            mockConfSection = new Mock<IConfigurationSection>();
        }

        public RepositoryDbContext CreateGetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<RepositoryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new RepositoryDbContext(options, mockConfSection.Object);
            
            return databaseContext;
        }
    }
}
