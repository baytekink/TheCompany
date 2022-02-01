using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.Products.Domain.Entity.Entities;
using System;
using System.Reflection;

namespace OnlineShop.Products.EntityFrameworkCore
{
    /// <summary>
    /// DB Operations Responsible
    /// </summary>
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        readonly IConfiguration configuration;

        public RepositoryDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
            Database.EnsureCreatedAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"), options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Find classes that implements IEntityTypeConfiguration<T>
            modelBuilder?.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
