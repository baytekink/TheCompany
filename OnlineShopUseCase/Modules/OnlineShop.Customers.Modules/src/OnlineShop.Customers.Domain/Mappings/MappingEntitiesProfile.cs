using AutoMapper;
using OnlineShop.Customers.Domain.Commands.Request;
using OnlineShop.Customers.Domain.Entity.Entities;
using OnlineShop.Customers.Domain.Queries.Response;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Customers.Domain.Mappings
{
    public class MappingEntitiesProfile : Profile
    {
        public MappingEntitiesProfile()
        {  
            CreateMap<CreateCustomerCommandRequest, Customer>();
            CreateMap<UpdateCustomerCommandRequest, Customer>();

            CreateMap<Customer, GetAllCustomerQueryResponse>();
            CreateMap<Customer, GetByIdCustomerQueryResponse>();

            CreateMap<Customer, CustomerChangedObject>();
        }  
    }
}
