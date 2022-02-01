using System.Threading.Tasks;
using OnlineShop.Products.Domain.Shared.Repository;
using OnlineShop.Products.Domain.Entity.Entities;
using AutoMapper;
using TheCompany.Domain.Shared.Common.Helper;
using OnlineShop.Products.Domain.Mappings;
using Moq;
using Xunit;
using OnlineShop.Products.Domain.Commands.Request; 
using TheCompany.Domain.Shared.Common.QueueMessaging.POCO;
using TheCompany.Domain.Shared.Common.QueueMessaging;

namespace OnlineShop.Products.Domain.Handlers.CommandHandlers
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository<Product>> _mockRepo;
        private readonly Mock<IProducer<ProductChangedObject>> _mockProducer;
        private readonly CreateProductCommandHandler handler;
        private readonly IIdGenerator idGenerator;
        private readonly IMapper mapper;
        private readonly IDateCreator dateCreator;

        public CreateProductCommandHandlerTest()
        {
            _mockRepo = new Mock<IProductRepository<Product>>();
            _mockProducer = new Mock<IProducer<ProductChangedObject>>();

            dateCreator = new DateCreatorUtc();
            idGenerator = new IdGenerator();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingEntitiesProfile());
            });
            mapper = mockMapper.CreateMapper();

            handler = new CreateProductCommandHandler(_mockRepo.Object, mapper, idGenerator, dateCreator, _mockProducer.Object);
        }

        #region Handle
        [Fact]
        public async Task Handle_ActionExecutes_ReturnsSuccess()
        {
            var request = new CreateProductCommandRequest()
            {
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = 1;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnsError()
        {
            var request = new CreateProductCommandRequest()
            {
                Title = "Stroller",
                Description = "Carries your baby in safe",
                Brand = "Mima",
                Model = "Zigi",
                Cost = 10,
                Price = 15
            };

            var response = 0;

            _mockRepo.Setup(repo => repo.CreateWithSaveAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));

            var result = await handler.Handle(request, default);

            //Assert  
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
        #endregion
    }
}
