using OnlineShop.Products.Domain.Commands.Request;
using OnlineShop.Products.Domain.Commands.Response;
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
using TheCompany.Domain.Shared.Common.Helper; 
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Products.Domain.Handlers.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IMapper mapper;
        readonly IProductRepository<Product> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<ProductChangedObject> producer;
        public UpdateProductCommandHandler(IProductRepository<Product> repository, IMapper mapper, IDateCreator dateCreator, IProducer<ProductChangedObject> producer)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dateCreator = dateCreator;
            this.producer = producer;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            int result = 0;

            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            if (obj != null && obj.IsDeleted == 0)
            {
                //may check any field is really updated

                var updateObj = mapper.Map<Product>(request);
                updateObj.CreateTime = obj.CreateTime;                
                updateObj.UpdateTime = dateCreator.CreateNow();

                result = await repository.UpdateWithSaveAsync(updateObj).ConfigureAwait(false);
                if (result == 1)
                    await producer.SendAsync(mapper.Map<ProductChangedObject>(updateObj)).ConfigureAwait(false);
            }

            return new UpdateProductCommandResponse
            {
                IsSuccess = result == 1
            };
        }
    }
}
