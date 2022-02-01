using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Orders.Domain.Shared.Enums
{
    public enum OrderStatus
    {
        None = 0,
        Suspend = 1,
        Completed = 2,
        Fail = 3
    }
}
