using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.Constants.LengthConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.DataLayer.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //add primary key
            builder.HasKey(e => e.Id);
            builder.HasMany(p => p.OrderItems).WithOne().HasForeignKey(p => p.OrderId);

            //add field requirements  

            //add indexes              
        }
    }
}
