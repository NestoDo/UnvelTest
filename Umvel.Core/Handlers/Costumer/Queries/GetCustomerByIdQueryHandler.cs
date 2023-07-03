using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Customer;
using Umvel.Infrastructure.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Customer.Queries
{
    public record GetCustomerByIdQuery(int costumerId) : IRequest<GetCustomerByIdResponse>;

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            Model.Customer customer = _customerRepository.GetById(request.costumerId);

            return _mapper.Map<GetCustomerByIdResponse>(customer);
        }
    }
}
