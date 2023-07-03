using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Sale;
using Umvel.Core.Handlers.Sale.Queries;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Xunit;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.UnitTests.Sale.Queries
{
    public class GetSaleByDateRangeQueryHandlerTests
    {
        private readonly GetSaleQueryHandler _getSaleQueryHandler;
        private readonly IMapper _mapper;
        private readonly Mock<ISaleRepository> _mockedSaleRepository;

        public GetSaleByDateRangeQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Sale, RegisterSaleRequest>().ReverseMap();
                cfg.CreateMap<Model.Sale, RegisterSaleResponse>().ReverseMap();
                cfg.CreateMap<Model.Sale, GetSaleResponse>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            _mockedSaleRepository = new Mock<ISaleRepository>();

            _getSaleQueryHandler = new GetSaleQueryHandler(_mockedSaleRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetSaleByIdQueryHandlerAsync_WithValidParameters_GetSaleById()
        {
            //Arrange
            GetSaleQuery getSaleQuery = new GetSaleQuery(DateTime.Now, DateTime.Now);

            List<Model.Sale> sales = new List<Model.Sale>();
            sales.Add(
                new Model.Sale()
                {
                    SaleId = 1,
                    Total = 10,
                    Date = DateTime.Now,
                    CustomerId = 0,
                    Concepts = new List<Model.Concept>(),
                    Customer = new Model.Customer()
                });

            _mockedSaleRepository.Setup(x => x.GetSaleByDateRange(getSaleQuery.startDate, getSaleQuery.endDate)).Returns(sales);

            //Act
            var getSaleResponse = await _getSaleQueryHandler.Handle(getSaleQuery, new CancellationToken());

            //Assert
            Assert.All(getSaleResponse,
                item => Assert.IsType<GetSaleResponse>(item)
            );
        }
    }
}
