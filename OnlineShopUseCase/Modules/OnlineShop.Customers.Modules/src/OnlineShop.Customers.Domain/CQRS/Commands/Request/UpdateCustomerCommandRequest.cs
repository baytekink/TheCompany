using OnlineShop.Customers.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Customers.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Customers.Domain.Commands.Request
{
    public class UpdateCustomerCommandRequest : CustomerCommonRequest, IRequest<UpdateCustomerCommandResponse>
    { 
        public Guid Id { get; set; } 
    }
}
