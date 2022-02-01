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
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public IReadOnlyList<CreateOrderItem> OrderItems { get; set; }         
    }
}
