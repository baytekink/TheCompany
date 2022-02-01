using OnlineShop.Products.Domain.Queries.Request;
using OnlineShop.Products.Domain.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Products.Domain.Shared.Repository;
using AutoMapper;
using OnlineShop.Products.Domain.Entity.Entities;

namespace OnlineShop.Products.Domain.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductRepository<Product> repository;
        readonly IMapper mapper;
        public GetByIdProductQueryHandler(IProductRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            return mapper.Map<GetByIdProductQueryResponse>(obj);
        }
    }
}
