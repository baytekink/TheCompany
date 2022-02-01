using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace TheCompany.Domain.Shared.Common.QueueMessaging.POCO
{
    public class ProductChangedObject
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }
    }
}
