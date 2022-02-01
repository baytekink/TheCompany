using AutoMapper;
using MassTransit;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCompany.Domain.Shared.Common.Helper; 
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Orders.Domain.QueueMessaging
{
    public class CustomerChangedEventConsumer : IConsumer<CustomerChangedObject>
    {
        readonly ICustomerRepository<Customer> repository;
        readonly IMapper mapper;
        readonly IDateCreator dateCreator;
        public CustomerChangedEventConsumer(ICustomerRepository<Customer> repository, IMapper mapper, IDateCreator dateCreator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dateCreator = dateCreator;
        }

        public async Task Consume(ConsumeContext<CustomerChangedObject> context)
        {
            var incomingObj = context?.Message;

            var customer = await repository.FindOneByConditionAsync(p => p.Id == incomingObj.Id).ConfigureAwait(false);
            if (customer != null)
            {
                //update or delete
                customer = mapper.Map(incomingObj, customer);
                customer.UpdateTime = dateCreator.CreateNow();

                await repository.UpdateWithSaveAsync(customer).ConfigureAwait(false);
            }
            else
            {
                //insert
                var mappedCustomer = mapper.Map<Customer>(incomingObj);
                mappedCustomer.CreateTime = dateCreator.CreateNow();

                await repository.CreateWithSaveAsync(mappedCustomer).ConfigureAwait(false);
            }
        }
    }
}
