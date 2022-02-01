using OnlineShop.Customers.Domain.Commands.Request;
using OnlineShop.Customers.Domain.Commands.Response;
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
using TheCompany.Domain.Shared.Common.Helper; 
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Customers.Domain.Handlers.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        readonly IIdGenerator idGenerator;
        readonly IMapper mapper;
        readonly ICustomerRepository<Customer> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<CustomerChangedObject> producer;

        public CreateCustomerCommandHandler(ICustomerRepository<Customer> repository, IMapper mapper, IIdGenerator idGenerator, IDateCreator dateCreator, IProducer<CustomerChangedObject> producer)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.idGenerator = idGenerator;
            this.dateCreator = dateCreator;
            this.producer = producer;
        }

        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            var id = idGenerator.GenerateId();
            var createObj = mapper.Map<Customer>(request);
            createObj.Id = id;
            createObj.CreateTime = dateCreator.CreateNow();

            var result = await repository.CreateWithSaveAsync(createObj).ConfigureAwait(false);

            bool IsSuccess = result == 1;
            if (IsSuccess) //send to queue
                await producer.SendAsync(mapper.Map<CustomerChangedObject>(createObj)).ConfigureAwait(false);

            return new CreateCustomerCommandResponse
            {
                IsSuccess = IsSuccess,
                Id = id
            };
        }
    }
}
