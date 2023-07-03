using AutoMapper;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Customer;
using Umvel.Core.Handlers.Customer.Queries;
using Umvel.Infrastructure.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Customer.Queries
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly GetCustomerByIdQueryHandler _getCustomerByIdQueryHandler;
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockedCustomerRepository;

        public GetCustomerByIdQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Model.Customer, RegisterCustomerResponse>().ReverseMap();
                cfg.CreateMap<Model.Customer, RegisterCustomerRequest>().ReverseMap();
                cfg.CreateMap<Model.Customer, GetCustomerByIdResponse>().ReverseMap();
                cfg.CreateMap<Model.Customer, GetAllCustomerResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedCustomerRepository = new Mock<ICustomerRepository>();

            _getCustomerByIdQueryHandler = new GetCustomerByIdQueryHandler(_mockedCustomerRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetCustomerByIdQueryHandlerAsync_WithValidParameters_GetCustomerById()
        {
            //Arrange
            Model.Customer customer = new Model.Customer()
            {
                CustomerId = 1,
                Name = "Omar",
                Sales = new List<Model.Sale>()
            };

            var getCustomerByIdQuery = new GetCustomerByIdQuery(1);

            _mockedCustomerRepository.Setup(x => x.GetById(customer.CustomerId)).Returns(customer);

            //Act
            var createProductResponse = await _getCustomerByIdQueryHandler.Handle(getCustomerByIdQuery, new CancellationToken());

            //Assert
            Assert.IsType<GetCustomerByIdResponse>(createProductResponse);
        }
    }
}
