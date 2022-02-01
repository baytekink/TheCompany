using OnlineShop.Products.Domain.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Products.Domain.Queries.Request
{
    public class GetAllProductQueryRequest : IRequest<IReadOnlyList<GetAllProductQueryResponse>>
    {

    }
}
