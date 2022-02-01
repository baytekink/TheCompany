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
    public class ProductChangedEventConsumer : IConsumer<ProductChangedObject>
    {
        readonly IProductRepository<Product> repository;
        readonly IMapper mapper;
        readonly IDateCreator dateCreator;
        public ProductChangedEventConsumer(IProductRepository<Product> repository, IMapper mapper, IDateCreator dateCreator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.dateCreator = dateCreator;
        }

        public async Task Consume(ConsumeContext<ProductChangedObject> context)
        {
            var incomingObj = context?.Message;

            var product = await repository.FindOneByConditionAsync(p => p.Id == incomingObj.Id).ConfigureAwait(false);
            if (product != null)
            {
                //update or delete
                product = mapper.Map(incomingObj, product);
                product.UpdateTime = dateCreator.CreateNow();

                await repository.UpdateWithSaveAsync(product).ConfigureAwait(false);
            }
            else
            {
                //insert
                var mappedProduct = mapper.Map<Product>(incomingObj);
                mappedProduct.CreateTime = dateCreator.CreateNow();

                await repository.CreateWithSaveAsync(mappedProduct).ConfigureAwait(false);
            }
        }
    }
}
