using TheCompany.Domain.Entity.Common.Entities;

namespace OnlineShop.Products.Domain.Entity.Entities
{
    public class Product : EntityBaseWithId
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }
    }
}