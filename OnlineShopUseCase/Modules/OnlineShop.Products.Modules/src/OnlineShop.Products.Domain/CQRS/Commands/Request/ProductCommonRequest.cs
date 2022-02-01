using OnlineShop.Products.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OnlineShop.Products.Domain.Shared.Constants.LengthConstants;

namespace OnlineShop.Products.Domain.Commands.Request
{
    public abstract class ProductCommonRequest
    {
        [Required]
        [MaxLength(ProductConstants.LENTitle)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ProductConstants.LENBrand)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ProductConstants.LENModel)]
        public string Model { get; set; }

        [MaxLength(ProductConstants.LENDescription)]
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
