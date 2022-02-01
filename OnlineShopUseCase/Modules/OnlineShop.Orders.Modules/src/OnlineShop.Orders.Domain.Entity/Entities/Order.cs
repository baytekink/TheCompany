using OnlineShop.Orders.Domain.Shared;
using OnlineShop.Orders.Domain.Shared.Enums;
using TheCompany.Domain.Entity.Common.AggregateRoots;
using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Orders.Domain.Entity.Entities
{
    public class Order : EntityBaseWithId, IAggregateRoot
    {
        public Guid CustomerId { get; set; }
        public virtual IReadOnlyList<OrderItem> OrderItems { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
    }
}