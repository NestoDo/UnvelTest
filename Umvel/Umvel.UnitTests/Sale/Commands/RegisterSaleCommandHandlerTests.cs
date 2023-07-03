using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Sale;
using Umvel.Contracts.Messages;
using Umvel.Core.Handlers.Sale.Commands;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Sale.Commands
{
    public class RegisterSaleCommandHandlerTests
    {
        private readonly RegisterSaleCommandHandler _registerSaleCommandHandler;
        private readonly IMapper _mapper;
        private readonly Mock<ISaleRepository> _mockedSaleRepository;

        public RegisterSaleCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg => {                ;
                cfg.CreateMap<Model.Sale, RegisterSaleRequest>().ReverseMap();
                cfg.CreateMap<Model.Sale, RegisterSaleResponse>().ReverseMap();
                cfg.CreateMap<Model.Sale, GetSaleResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedSaleRepository = new Mock<ISaleRepository>();

            _registerSaleCommandHandler = new RegisterSaleCommandHandler(_mockedSaleRepository.Object, _mapper);
        }

        [Fact]
        public async Task RegisterSaleCommandHandlerAsync_WithNullParameter_ThrowsException()
        {
            //Arrange
            RegisterSaleCommand command = null;

            //Act
            var actualException = await Assert.ThrowsAsync<Exception>(() => _registerSaleCommandHandler.Handle(command, new CancellationToken()));

            //Assert
            Assert.Equal(MessageConstants.RegisterSaleCommandNull, actualException.Message);
        }

        [Fact]
        public async Task RegisterSaleCommandHandlerAsync_WithValidParameters_CreatesSale()
        {
            //Arrange
            var registerSaleRequest = new RegisterSaleRequest()
            {
                Date = DateTime.Now,
                Total = 10
            };

            Model.Sale sale = new Model.Sale()
            {
                SaleId = 1,
                Date = DateTime.Now,
                Total = 10,
                CustomerId = 1,
                Customer = new Model.Customer(),
                Concepts = new List<Model.Concept>()
            };

            var registerSaleCommand = new RegisterSaleCommand(registerSaleRequest);

            _mockedSaleRepository.Setup(x => x.Add(It.IsAny<Model.Sale>())).Returns(sale);

            //Act
            var createSaleResponse = await _registerSaleCommandHandler.Handle(registerSaleCommand, new CancellationToken());

            //Assert
            Assert.IsType<RegisterSaleResponse>(createSaleResponse);

        }
    }
}
