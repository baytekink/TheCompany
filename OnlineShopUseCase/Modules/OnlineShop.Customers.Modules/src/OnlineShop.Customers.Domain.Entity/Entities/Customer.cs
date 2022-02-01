using OnlineShop.Customers.Domain.Shared.Enums;
using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Customers.Domain.Entity.Entities
{
    public class Customer: EntityBaseWithId
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }
        public CustomerGender Gender { get; set; }

    }
}