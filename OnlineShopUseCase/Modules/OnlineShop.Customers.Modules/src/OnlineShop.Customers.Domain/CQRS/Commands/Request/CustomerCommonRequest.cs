using OnlineShop.Customers.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using OnlineShop.Customers.Domain.Shared.Constants.LengthConstants;

namespace OnlineShop.Customers.Domain.Commands.Request
{
    public abstract class CustomerCommonRequest  
    {
        [Required]
        [MaxLength(CustomerConstants.LENName)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CustomerConstants.LENSurname)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(CustomerConstants.LENAddress)]
        public string Address { get; set; }

        [Required]
        [MaxLength(CustomerConstants.LENPhone)]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [EnumDataType(typeof(CustomerGender))]
        public CustomerGender Gender { get; set; }
    }
}
