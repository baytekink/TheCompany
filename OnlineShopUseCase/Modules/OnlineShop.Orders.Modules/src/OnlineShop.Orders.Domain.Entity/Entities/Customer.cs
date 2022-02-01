using OnlineShop.Orders.Domain.Shared;
using OnlineShop.Orders.Domain.Shared.Enums;
using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Orders.Domain.Entity.Entities
{
    public class Customer: EntityBaseWithId
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; } 
    }
}