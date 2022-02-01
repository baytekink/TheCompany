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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IIdGenerator idGenerator;
        readonly IMapper mapper;
        readonly IProductRepository<Product> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<ProductChangedObject> producer;
        public CreateProductCommandHandler(IProductRepository<Product> repository, IMapper mapper, IIdGenerator idGenerator, IDateCreator dateCreator, IProducer<ProductChangedObject> producer)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.idGenerator = idGenerator;
            this.dateCreator = dateCreator;
            this.producer = producer;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var id = idGenerator.GenerateId();
            var createObj = mapper.Map<Product>(request);
            createObj.Id = id;
            createObj.CreateTime = dateCreator.CreateNow();

            var result = await repository.CreateWithSaveAsync(createObj).ConfigureAwait(false);
            bool IsSuccess = result == 1;
            if (IsSuccess)
                await producer.SendAsync(mapper.Map<ProductChangedObject>(createObj)).ConfigureAwait(false);

            return new CreateProductCommandResponse
            {
                IsSuccess = IsSuccess,
                Id = id
            };
        }
    }
}
