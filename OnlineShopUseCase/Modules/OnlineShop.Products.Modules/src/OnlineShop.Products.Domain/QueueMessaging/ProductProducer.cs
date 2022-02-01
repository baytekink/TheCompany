using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using TheCompany.Domain.Shared.Common.QueueMessaging;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Products.Domain.QueueMessaging
{ 
    public class ProductProducer : IProducer<ProductChangedObject>
    { 
        readonly ISendEndpointProvider sendEndpointProvider;
        public ProductProducer(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendAsync(ProductChangedObject producedObj)
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{QueueMessagingSettings.ProductChangedEventQueue}")).ConfigureAwait(false);
            await sendEndpoint.Send(producedObj).ConfigureAwait(false);            
        }
    }
}
