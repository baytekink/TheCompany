using OnlineShop.Orders.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Orders.Domain.Shared.ViewModels
{  
    public class OrderItemVM
    {
        public Guid ProductId { get; set; }
        public ProductVM Product { get; set; }         
        public int Count { get; set; }
        public decimal Price { get; set; }
    }     
}
