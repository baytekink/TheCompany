using OnlineShop.Orders.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Orders.Domain.Shared.QueueMessaging
{
    public class OrderCreatedObject
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public IReadOnlyList<OrderCreatedObjectItem> OrderItems { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
