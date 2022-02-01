using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Customers.Domain.Entity.Entities;
using OnlineShop.Customers.Domain.Shared.Constants.LengthConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.DataLayer.Configurations
{
    internal class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //add primary key
            builder.HasKey(e => e.Id);

            //add field requirements
            builder.Property(x => x.Name).IsRequired().HasMaxLength(CustomerConstants.LENName);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(CustomerConstants.LENSurname);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Address).IsRequired().HasMaxLength(CustomerConstants.LENAddress);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(CustomerConstants.LENPhone);
            builder.Property(x => x.Gender).IsRequired();             

            //add indexes             
            //builder.HasIndex(e => e.BirthDate); //get from bdate
        }
    }
}
