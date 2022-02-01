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
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        readonly IMapper mapper;
        readonly ICustomerRepository<Customer> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<CustomerChangedObject> producer;
        public UpdateCustomerCommandHandler(ICustomerRepository<Customer> repository, IMapper mapper, IDateCreator dateCreator, IProducer<CustomerChangedObject> producer)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dateCreator = dateCreator;
            this.producer = producer;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            int result = 0;

            var obj = await repository.FindOneByConditionAsync(p => p.Id == request.Id).ConfigureAwait(false);
            if (obj != null && obj.IsDeleted == 0)
            {
                //may check any field is really updated

                var updateObj = mapper.Map<Customer>(request);
                updateObj.CreateTime = obj.CreateTime;
                updateObj.UpdateTime = dateCreator.CreateNow();

                result = await repository.UpdateWithSaveAsync(updateObj).ConfigureAwait(false);
                if (result == 1)
                    await producer.SendAsync(mapper.Map<CustomerChangedObject>(updateObj)).ConfigureAwait(false);
            }

            return new UpdateCustomerCommandResponse
            {
                IsSuccess = result == 1
            };
        }
    }
}
