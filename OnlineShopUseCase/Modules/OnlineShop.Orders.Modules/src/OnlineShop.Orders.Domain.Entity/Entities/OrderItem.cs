using OnlineShop.Orders.Domain.Shared;
using OnlineShop.Orders.Domain.Shared.Enums;
using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Orders.Domain.Entity.Entities
{ 
    public class OrderItem: EntityBase
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}