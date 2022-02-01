using OnlineShop.Orders.Domain.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Orders.MongoDB;
using MongoDB.Driver; 
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Repository.Repository
{
    public class OrderRepositoryQuery : IOrderNoSqlRepository<OrderFulFilledVM>
    {
        readonly INoSqlService mongoService;
        public OrderRepositoryQuery(INoSqlService mongoService)
        {
            this.mongoService = mongoService;
        }

        public async Task CreateOrderFulFilledVM(OrderFulFilledVM order)
        {
            await this.mongoService.GetCollection<OrderFulFilledVM>().InsertOneAsync(order).ConfigureAwait(false);
        }

        public async Task<OrderFulFilledVM> FindOneOrderFullFilledAsync(Guid orderId)
        {
            var item = await this.mongoService.GetCollection<OrderFulFilledVM>().Find(p => p.Id == orderId).Limit(1).SingleAsync().ConfigureAwait(false);
            return item;
        }

        public async Task<IReadOnlyList<OrderFulFilledVM>> FindOrderFullFilledAsync(DateTime startDate, DateTime endDate)
        { 
            var items = await this.mongoService.GetCollection<OrderFulFilledVM>().Find(p => p.CreateTime >= startDate && p.CreateTime <= endDate).ToListAsync().ConfigureAwait(false);
            return items;
        }
    }
}
