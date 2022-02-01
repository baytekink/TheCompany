using OnlineShop.Customers.Domain.Queries.Request;
using OnlineShop.Customers.Domain.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Repository;
using AutoMapper;
using OnlineShop.Customers.Domain.Entity.Entities;

namespace OnlineShop.Customers.Domain.Handlers.QueryHandlers
{
    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQueryRequest, GetByIdCustomerQueryResponse>
    {
        readonly ICustomerRepository<Customer> repository;
        readonly IMapper mapper;
        public GetByIdCustomerQueryHandler(ICustomerRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GetByIdCustomerQueryResponse> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            return mapper.Map<GetByIdCustomerQueryResponse>(obj);
        }
    }
}
