using OnlineShop.Orders.Domain.Queries.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using AutoMapper;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.ViewModels;

namespace OnlineShop.Orders.Domain.Handlers.QueryHandlers
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, OrderFulFilledVM>
    {
        readonly IOrderNoSqlRepository<OrderFulFilledVM> repository;
        public GetByIdOrderQueryHandler(IOrderNoSqlRepository<OrderFulFilledVM> repository)
        {
            this.repository = repository;
        }

        public async Task<OrderFulFilledVM> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            if (request != null)
                return await repository.FindOneOrderFullFilledAsync(request.Id).ConfigureAwait(false);

            return null;
        }
    }
}
