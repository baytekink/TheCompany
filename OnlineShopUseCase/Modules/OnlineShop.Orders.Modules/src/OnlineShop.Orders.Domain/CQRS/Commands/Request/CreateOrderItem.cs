using OnlineShop.Orders.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Orders.Domain.Commands.Request
{
    public class CreateOrderItem
    {
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]        
        public int Count { get; set; }
        
        [Required]
        public decimal Price { get; set; }
    }
}
