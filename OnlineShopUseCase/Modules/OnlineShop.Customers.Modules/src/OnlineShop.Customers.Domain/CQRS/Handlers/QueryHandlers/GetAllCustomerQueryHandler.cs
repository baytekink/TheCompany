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
using OnlineShop.Customers.Domain.Entity.Entities;
using AutoMapper;

namespace OnlineShop.Customers.Domain.Handlers.QueryHandlers
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, IReadOnlyList<GetAllCustomerQueryResponse>>
    { 
        readonly ICustomerRepository<Customer> repository;
        readonly IMapper mapper;
        public GetAllCustomerQueryHandler(ICustomerRepository<Customer> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await repository.FindAllAsync().ConfigureAwait(false);
            return mapper.Map<IReadOnlyList<GetAllCustomerQueryResponse>>(list);
        }
    }
}
