using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Product;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Product.Queries
{
    public record GetProductByIdQuery(int costumerId) : IRequest<GetProductByIdResponse>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Model.Product product = _productRepository.GetById(request.costumerId);

            return _mapper.Map<GetProductByIdResponse>(product);
        }
    }
}
