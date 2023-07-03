using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Sale;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Sale.Queries
{
    public record GetSaleQuery(DateTime startDate, DateTime endDate) : IRequest<IEnumerable<GetSaleResponse>> { }

    public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, IEnumerable<GetSaleResponse>>
    {
        private ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetSaleResponse>> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Model.Sale> saleList = _saleRepository.GetSaleByDateRange(request.startDate, request.endDate);

            return _mapper.Map<IEnumerable<GetSaleResponse>>(saleList);
        }
    }
}
