using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.Orders.Domain.Entity.Entities;
using System;
using System.Reflection;

namespace OnlineShop.Orders.EntityFrameworkCore
{
    /// <summary>
    /// DB Operations Responsible
    /// </summary>
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }

        readonly IConfiguration configuration;

        public RepositoryDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
            Database.EnsureCreatedAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connString))
                optionsBuilder.UseSqlite(connString, options =>
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
