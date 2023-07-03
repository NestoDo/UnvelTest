using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Sale;
using Umvel.Contracts.Messages;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Sale.Commands
{
    public record RegisterSaleCommand(RegisterSaleRequest model) : IRequest<RegisterSaleResponse>;

    public class RegisterSaleCommandHandler : IRequestHandler<RegisterSaleCommand, RegisterSaleResponse>
    {
        private ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public RegisterSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<RegisterSaleResponse> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new Exception(MessageConstants.RegisterSaleCommandNull);

            var requestModel = request.model;

            var saleModel = _mapper.Map<Model.Sale>(requestModel);

            var sale = _saleRepository.Add(saleModel);

            return _mapper.Map<RegisterSaleResponse>(sale);
        }
    }
}
