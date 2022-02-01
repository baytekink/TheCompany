using OnlineShop.Orders.Domain.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TheCompany.Domain.Shared.Common.Repository;

namespace OnlineShop.Orders.Domain.Shared.Repository
{
    public interface IOrderNoSqlRepository<T>  
    {
        Task CreateOrderFulFilledVM(OrderFulFilledVM order);
        Task<IReadOnlyList<OrderFulFilledVM>> FindOrderFullFilledAsync(DateTime startDate, DateTime endDate);
        Task<OrderFulFilledVM> FindOneOrderFullFilledAsync(Guid orderId);
    }
}
