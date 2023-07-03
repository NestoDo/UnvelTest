using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Product;
using Umvel.Core.Handlers.Product.Queries;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Product.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler;
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockedProductRepository;

        public GetProductByIdQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Product, RegisterProductResponse>().ReverseMap();
                cfg.CreateMap<Model.Product, RegisterProductRequest>().ReverseMap();
                cfg.CreateMap<Model.Product, GetProductByIdResponse>().ReverseMap();
                cfg.CreateMap<Model.Product, GetAllProductResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedProductRepository = new Mock<IProductRepository>();

            _getProductByIdQueryHandler = new GetProductByIdQueryHandler(_mockedProductRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetProductByIdQueryHandlerAsync_WithValidParameters_GetProductById()
        {
            //Arrange
            Model.Product customer = new Model.Product()
            {
                ProductId = 1,
                Cost = 10,
                Name = "Prouct 1",
                UnitPrice = "10",
                Concepts = new List<Model.Concept>()
            };

            var getProductByIdQuery = new GetProductByIdQuery(1);

            _mockedProductRepository.Setup(x => x.GetById(customer.ProductId)).Returns(customer);

            //Act
            var createProductResponse = await _getProductByIdQueryHandler.Handle(getProductByIdQuery, new CancellationToken());

            //Assert
            Assert.IsType<GetProductByIdResponse>(createProductResponse);
        }
    }
}

