using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Product;
using Umvel.Contracts.Messages;
using Umvel.Core.Handlers.Product.Commands;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Product.Commads
{
    public class RegisterProductCommandHandlerTests
    {
        private readonly RegisterProductCommandHandler _registerProductCommandHandler;
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockedProductRepository;

        public RegisterProductCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Model.Product, RegisterProductResponse>().ReverseMap();
                cfg.CreateMap<Model.Product, RegisterProductRequest>().ReverseMap();
                cfg.CreateMap<Model.Product, GetProductByIdResponse>().ReverseMap();
                cfg.CreateMap<Model.Product, GetAllProductResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedProductRepository = new Mock<IProductRepository>();

            _registerProductCommandHandler = new RegisterProductCommandHandler(_mockedProductRepository.Object, _mapper);
        }

        [Fact]
        public async Task RegisterProductCommandHandlerAsync_WithNullParameter_ThrowsException()
        {
            //Arrange
            RegisterProductCommand command = null;

            //Act
            var actualException = await Assert.ThrowsAsync<Exception>(() => _registerProductCommandHandler.Handle(command, new CancellationToken()));

            //Assert
            Assert.Equal(MessageConstants.RegisterProductCommandNull, actualException.Message);
        }

        [Fact]
        public async Task RegisterProductCommandHandlerAsync_WithValidParameters_CreatesProduct()
        {
            //Arrange
            var registerProductRequest = new RegisterProductRequest()
            {
                Cost = 10,
                UnitPrice = "10"
            };

            Model.Product product = new Model.Product()
            {
                ProductId = 1,
                Cost = 10,
                Name = "Product 1",
                UnitPrice = "10",
                Concepts  = new List<Model.Concept>()
            };

            var registerProductCommand = new RegisterProductCommand(registerProductRequest);

            _mockedProductRepository.Setup(x => x.Add(It.IsAny<Model.Product>())).Returns(product);

            //Act
            var createProductResponse = await _registerProductCommandHandler.Handle(registerProductCommand, new CancellationToken());

            //Assert
            Assert.IsType<RegisterProductResponse>(createProductResponse);

        }
    }
}
