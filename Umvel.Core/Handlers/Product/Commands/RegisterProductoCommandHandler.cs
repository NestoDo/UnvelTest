using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umvel.Contracts.DTO.Product;
using Umvel.Contracts.Messages;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Product.Commands
{
    public record RegisterProductCommand(RegisterProductRequest model) : IRequest<RegisterProductResponse>;

    public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, RegisterProductResponse>
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public RegisterProductCommandHandler(IProductRepository customerRepository, IMapper mapper)
        {
            _productRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<RegisterProductResponse> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new Exception(MessageConstants.RegisterProductCommandNull);

            var requestModel = request.model;

            var productModel = _mapper.Map<Model.Product>(requestModel);

            var product =_productRepository.Add(productModel);

            return _mapper.Map<RegisterProductResponse>(product);
        }
    }
}
