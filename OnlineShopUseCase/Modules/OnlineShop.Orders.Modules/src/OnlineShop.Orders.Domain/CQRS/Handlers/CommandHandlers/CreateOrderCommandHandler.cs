using OnlineShop.Orders.Domain.Commands.Request;
using OnlineShop.Orders.Domain.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Orders.Domain.Shared.Enums;
using MassTransit; 
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Orders.Domain.Handlers.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IIdGenerator idGenerator;
        readonly IMapper mapper;
        readonly IOrderRepository<Order> repository;
        readonly IDateCreator dateCreator;
        readonly IProducer<OrderCreatedObject> producer;

        public CreateOrderCommandHandler(IOrderRepository<Order> repository, IMapper mapper, IIdGenerator idGenerator, IDateCreator dateCreator, IProducer<OrderCreatedObject> producer)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.idGenerator = idGenerator;
            this.dateCreator = dateCreator;
            this.producer = producer;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var id = idGenerator.GenerateId();
            var createObj = mapper.Map<Order>(request);
            createObj.Id = id;
            createObj.CreateTime = dateCreator.CreateNow();
            createObj.OrderStatus = OrderStatus.Suspend;

            Tuple<List<OrderItem>, decimal> orderItemsTuple = BuildOrderItemsAndCalculateTotalPrice(id, request?.OrderItems);
            createObj.OrderItems = orderItemsTuple.Item1;
            createObj.TotalPrice = orderItemsTuple.Item2;

            var result = await repository.CreateWithSaveAsync(createObj).ConfigureAwait(false);
            bool IsSuccess = result >= 1;
            if (IsSuccess)
                await producer.SendAsync(mapper.Map<OrderCreatedObject>(createObj)).ConfigureAwait(false);

            return new CreateOrderCommandResponse
            {
                IsSuccess = IsSuccess,
                Id = id
            };
        }

        private Tuple<List<OrderItem>, decimal> BuildOrderItemsAndCalculateTotalPrice(Guid orderId, IReadOnlyList<CreateOrderItem> pOrderItems)
        {
            List<OrderItem> orderItems = new();
            decimal totalPrice = 0;
            foreach (var item in pOrderItems)
            {
                var t = mapper.Map<OrderItem>(item);
                t.OrderId = orderId;
                orderItems.Add(t);

                totalPrice += item.Count * item.Price;
            }

            return new Tuple<List<OrderItem>, decimal>(orderItems, totalPrice);
        }
    }
}
