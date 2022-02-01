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
using TheCompany.Domain.Shared.Common.Helper;
using AutoMapper;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Products.Domain.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        readonly IProductRepository<Product> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<ProductChangedObject> producer;
        readonly IMapper mapper;
        public DeleteProductCommandHandler(IProductRepository<Product> repository, IDateCreator dateCreator, IMapper mapper, IProducer<ProductChangedObject> producer)
        {
            this.repository = repository;
            this.dateCreator = dateCreator;
            this.mapper = mapper;
            this.producer = producer; 
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            int result = 0;

            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            if (obj != null && obj.IsDeleted == 0)
            {
                obj.UpdateTime = dateCreator.CreateNow();
                obj.IsDeleted = 1;

                result = await repository.UpdateWithSaveAsync(obj).ConfigureAwait(false);
                if (result == 1)
                    await producer.SendAsync(mapper.Map<ProductChangedObject>(obj)).ConfigureAwait(false);
            }

            return new DeleteProductCommandResponse
            {
                IsSuccess = result == 1
            };
        }
    }
}
