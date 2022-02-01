using AutoMapper;
using OnlineShop.Products.Domain.Commands.Request;
using OnlineShop.Products.Domain.Entity.Entities;
using OnlineShop.Products.Domain.Queries.Response;
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;

namespace OnlineShop.Products.Domain.Mappings
{
    public class MappingEntitiesProfile : Profile
    {
        public MappingEntitiesProfile()
        {
            CreateMap<CreateProductCommandRequest, Product>();
            CreateMap<UpdateProductCommandRequest, Product>();

            CreateMap<Product, GetAllProductQueryResponse>();
            CreateMap<Product, GetByIdProductQueryResponse>();

            CreateMap<Product, ProductChangedObject>();
        }
    }
}
