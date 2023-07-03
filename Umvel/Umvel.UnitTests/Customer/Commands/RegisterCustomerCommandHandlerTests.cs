using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Customer;
using Umvel.Contracts.Messages;
using Umvel.Core.Handlers.Customer.Commands;
using Umvel.Infrastructure.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Customer.Commands
{
    public class RegisterCustomerCommandHandlerTests
    {
        private readonly RegisterCustomerCommandHandler _registerCustomerCommandHandler;
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockedCustomerRepository;

        public RegisterCustomerCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Model.Customer, RegisterCustomerResponse>().ReverseMap();
                cfg.CreateMap<Model.Customer, RegisterCustomerRequest>().ReverseMap();
                cfg.CreateMap<Model.Customer, GetCustomerByIdResponse>().ReverseMap();
                cfg.CreateMap<Model.Customer, GetAllCustomerResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedCustomerRepository =  new Mock<ICustomerRepository>();

            _registerCustomerCommandHandler = new RegisterCustomerCommandHandler(_mockedCustomerRepository.Object, _mapper);
        }

        [Fact]
        public async Task RegisterCustomerCommandHandlerAsync_WithNullParameter_ThrowsException()
        {
            //Arrange
            RegisterCustomerCommand command = null;

            //Act
            var actualException = await Assert.ThrowsAsync<Exception>(() => _registerCustomerCommandHandler.Handle(command, new CancellationToken()));

            //Assert
            Assert.Equal(MessageConstants.RegisterCustomerCommandNull, actualException.Message);
        }

        [Fact]
        public async Task RegisterCustomerCommandHandlerAsync_WithValidParameters_CreatesCustomer()
        {
            //Arrange
            var registerCustomerRequest = new RegisterCustomerRequest()
            {
                Name = "Omar"
            };

            Model.Customer customer = new Model.Customer()
            {
                CustomerId = 1,
                Name = registerCustomerRequest.Name,
                Sales = new List<Model.Sale>()
            };

            var registerCustomerCommand = new RegisterCustomerCommand(registerCustomerRequest);

            _mockedCustomerRepository.Setup(x => x.Add(It.IsAny<Model.Customer>())).Returns(customer);
            
            //Act
            var createProductResponse = await _registerCustomerCommandHandler.Handle(registerCustomerCommand, new CancellationToken());

            //Assert
            Assert.IsType<RegisterCustomerResponse>(createProductResponse);
        }
    }
}
