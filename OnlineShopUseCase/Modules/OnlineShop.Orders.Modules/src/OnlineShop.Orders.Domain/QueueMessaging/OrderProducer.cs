using MassTransit;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.Repository; 
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Products.Domain.QueueMessaging
{
    public class OrderProducer : IProducer<OrderCreatedObject>
    {
        readonly ISendEndpointProvider sendEndpointProvider;
        public OrderProducer(ISendEndpointProvider sendEndpointProvider)
        {
            this.sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendAsync(OrderCreatedObject producedObj)
        {
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{QueueMessagingOrderSettings.OrderCreatedEventQueue}")).ConfigureAwait(false);
            await sendEndpoint.Send(producedObj).ConfigureAwait(false);            
        }
    }
}
