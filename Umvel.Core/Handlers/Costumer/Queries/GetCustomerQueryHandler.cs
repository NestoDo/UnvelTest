using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Customer;
using Umvel.Infrastructure.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Customer.Queries
{
    public record GetAllCustomerQuery : IRequest<IEnumerable<GetAllCustomerResponse>> { }

    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<GetAllCustomerResponse>>
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllCustomerResponse>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Model.Customer> customerList = _customerRepository.GetAll();

            return _mapper.Map<IEnumerable<GetAllCustomerResponse>>(customerList);
        }
    }
}
