using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Products.Domain.Entity.Entities;
using OnlineShop.Products.Domain.Shared.Constants.LengthConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.DataLayer.Configurations
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //add primary key
            builder.HasKey(e => e.Id);

            //add field requirements
            builder.Property(x => x.Title).IsRequired().HasMaxLength(ProductConstants.LENTitle);
            builder.Property(x => x.Brand).IsRequired().HasMaxLength(ProductConstants.LENBrand );
            builder.Property(x => x.Model).IsRequired().HasMaxLength(ProductConstants.LENModel);
            builder.Property(x => x.Cost).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(ProductConstants.LENDescription);

            //add indexes             
            //builder.HasIndex(e => e.BirthDate); //get from bdate
        }
    }
}
