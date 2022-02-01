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
using TheCompany.Domain.Shared.Common.Helper; 
using AutoMapper;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Customers.Domain.Handlers.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        readonly ICustomerRepository<Customer> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<CustomerChangedObject> producer;
        readonly IMapper mapper;
        public DeleteCustomerCommandHandler(ICustomerRepository<Customer> repository, IDateCreator dateCreator, IMapper mapper, IProducer<CustomerChangedObject> producer)
        {
            this.repository = repository;
            this.dateCreator = dateCreator;
            this.producer = producer;
            this.mapper = mapper;
        }

        public async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            int result = 0;

            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            if (obj != null && obj.IsDeleted == 0)
            {
                obj.UpdateTime = dateCreator.CreateNow();
                obj.IsDeleted = 1;

                result = await repository.UpdateWithSaveAsync(obj).ConfigureAwait(false);
                if (result == 1)
                    await producer.SendAsync(mapper.Map<CustomerChangedObject>(obj)).ConfigureAwait(false);
            }

            return new DeleteCustomerCommandResponse
            {
                IsSuccess = result == 1
            };
        }
    }
}
