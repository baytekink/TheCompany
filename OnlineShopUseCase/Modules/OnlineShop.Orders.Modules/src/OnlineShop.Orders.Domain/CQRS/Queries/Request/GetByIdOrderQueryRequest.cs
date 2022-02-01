using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Domain.Queries.Request
{
    public class GetByIdOrderQueryRequest : IRequest<OrderFulFilledVM>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
