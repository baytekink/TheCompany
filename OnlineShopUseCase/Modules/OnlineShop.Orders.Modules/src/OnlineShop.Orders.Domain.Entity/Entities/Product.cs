using OnlineShop.Orders.Domain.Shared;
using OnlineShop.Orders.Domain.Shared.Enums;
using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Orders.Domain.Entity.Entities
{
    public class Product: EntityBaseWithId
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public string Description { get; set; } 
    }
}