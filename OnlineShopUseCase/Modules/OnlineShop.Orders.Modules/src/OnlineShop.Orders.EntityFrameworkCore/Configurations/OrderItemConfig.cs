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
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //add primary key
            builder.HasKey(e => new { e.OrderId, e.ProductId });            
            //add field requirements

            //add indexes              
        }
    }
}
