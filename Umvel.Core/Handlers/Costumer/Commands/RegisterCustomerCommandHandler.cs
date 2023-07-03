using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Customer;
using Umvel.Infrastructure.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Customer.Commands
{
    public record RegisterCustomerCommand(RegisterCustomerRequest model) : IRequest<RegisterCustomerResponse>;

    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<RegisterCustomerResponse> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var requestModel = request.model;

            var customer = _mapper.Map<Model.Customer>(requestModel);

            _customerRepository.Add(customer);          

            return _mapper.Map<RegisterCustomerResponse>(customer);
        }
    }
}
