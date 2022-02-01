using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCompany.Domain.Shared.Common.QueueMessaging;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Customers.Domain.QueueMessaging
{
    public class CustomerProducer : IProducer<CustomerChangedObject>
    {
        readonly ISendEndpointProvider sendEndpointProvider;
        public CustomerProducer(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendAsync(CustomerChangedObject producedObj)
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{QueueMessagingSettings.CustomerChangedEventQueue}")).ConfigureAwait(false);
            await sendEndpoint.Send(producedObj).ConfigureAwait(false);            
        }
    }
}
