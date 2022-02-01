using OnlineShop.Products.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations;
using OnlineShop.Products.Domain.Shared.Constants.LengthConstants;

namespace OnlineShop.Products.Domain.Commands.Request
{
    public class UpdateProductCommandRequest : ProductCommonRequest, IRequest<UpdateProductCommandResponse>
    { 
        public Guid Id { get; set; } 
    }
}
