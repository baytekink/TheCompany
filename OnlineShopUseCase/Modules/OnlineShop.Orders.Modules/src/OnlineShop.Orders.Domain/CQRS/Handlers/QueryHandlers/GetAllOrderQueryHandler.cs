using OnlineShop.Orders.Domain.Queries.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Entity.Entities;
using AutoMapper;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Domain.Handlers.QueryHandlers
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, IReadOnlyList<OrderFulFilledVM>>
    {
        readonly IOrderNoSqlRepository<OrderFulFilledVM> repository;        
        public GetAllOrderQueryHandler(IOrderNoSqlRepository<OrderFulFilledVM> repository )
        {
            this.repository = repository;            
        }

        public async Task<IReadOnlyList<OrderFulFilledVM>> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            IReadOnlyList<OrderFulFilledVM> list;
            if (request != null)
                list = await repository.FindOrderFullFilledAsync(request.StartDate, request.EndDate).ConfigureAwait(false);
            else
                list = new List<OrderFulFilledVM>();

            return list;
        }
    }
}
