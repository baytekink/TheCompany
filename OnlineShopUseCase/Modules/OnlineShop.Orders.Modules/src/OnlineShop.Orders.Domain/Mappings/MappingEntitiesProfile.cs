using AutoMapper;
using OnlineShop.Orders.Domain.Commands.Request;
using OnlineShop.Orders.Domain.Entity.Entities;
using OnlineShop.Orders.Domain.Shared.QueueMessaging;
using OnlineShop.Orders.Domain.Shared.ViewModels;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Orders.Domain.Mappings
{
    public class MappingEntitiesProfile : Profile
    {
        public MappingEntitiesProfile()
        {
            CreateMap<CreateOrderCommandRequest, Order>();
            CreateMap<CreateOrderItem, OrderItem>();

            CreateMap<CustomerChangedObject, Customer>();
            CreateMap<ProductChangedObject, Product>();

            CreateMap<Order, OrderCreatedObject>();
            CreateMap<OrderItem, OrderCreatedObjectItem>();

            CreateMap<OrderCreatedObject, OrderFulFilledVM>();
            CreateMap<OrderCreatedObjectItem, OrderItemVM>();

            CreateMap<Customer, CustomerVM>();
            CreateMap<Product, ProductVM>();
        }
    }
}
