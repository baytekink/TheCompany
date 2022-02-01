using AutoMapper;
using MassTransit;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.Repository;
using OnlineShop.Orders.Domain.Shared.ViewModels; 

namespace OnlineShop.Orders.Domain.QueueMessaging
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedObject>
    {
        readonly IOrderNoSqlRepository<OrderFulFilledVM> orderRepository;
        readonly ICustomerRepository<Customer> customerRepository;
        readonly IProductRepository<Product> productRepository;
        readonly IMapper mapper;
        public OrderCreatedEventConsumer(IOrderNoSqlRepository<OrderFulFilledVM> orderRepository, ICustomerRepository<Customer> customerRepository, IProductRepository<Product> productRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderCreatedObject> context)
        {
            var incomingObj = context?.Message;
            //build whole orderfulfilled view model
            var fullFilledOrder = mapper.Map<OrderFulFilledVM>(incomingObj);


            //find customer
            var customer = await customerRepository.FindOneByConditionAsync(p => p.Id == incomingObj.CustomerId).ConfigureAwait(false);
            if (customer != null)
                fullFilledOrder.Customer = mapper.Map<CustomerVM>(customer);

            //find products
            var products = await productRepository.FindByConditionAsync(p => incomingObj.OrderItems.Select(p => p.ProductId).Contains(p.Id)).ConfigureAwait(false);                                   
            if (products != null && products.Any())
                MapProductsToOrderItems(products, fullFilledOrder.OrderItems);

            //now our fulfilled order is ready, write to db to query later
            await orderRepository.CreateOrderFulFilledVM(fullFilledOrder).ConfigureAwait(false);
        }

        void MapProductsToOrderItems(IReadOnlyList<Product> products, IReadOnlyList<OrderItemVM> orderItems)
        {
            var productsDic = products.ToDictionary(p => p.Id);
            foreach (var orderItem in orderItems)
            {
                if (productsDic.TryGetValue(orderItem.ProductId, out Product pr))
                    orderItem.Product = mapper.Map<ProductVM>(pr);
            }
        }
    }
}
