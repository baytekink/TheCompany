using OnlineShop.Customers.Domain.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Customers.Domain.Queries.Request
{
    public class GetAllCustomerQueryRequest : IRequest<IReadOnlyList<GetAllCustomerQueryResponse>>
    {

    }
}
