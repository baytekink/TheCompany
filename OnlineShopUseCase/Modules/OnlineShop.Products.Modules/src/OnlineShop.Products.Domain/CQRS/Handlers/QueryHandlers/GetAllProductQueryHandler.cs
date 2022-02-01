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
using OnlineShop.Products.Domain.Entity.Entities;
using AutoMapper;

namespace OnlineShop.Products.Domain.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IReadOnlyList<GetAllProductQueryResponse>>
    { 
        readonly IProductRepository<Product> repository;
        readonly IMapper mapper;
        public GetAllProductQueryHandler(IProductRepository<Product> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await repository.FindAllAsync().ConfigureAwait(false);
            return mapper.Map<IReadOnlyList<GetAllProductQueryResponse>>(list);
        }
    }
}
