using AutoMapper;
using MediatR;
using Umvel.Contracts.DTO.Product;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Handlers.Product.Queries
{
    public record GetAllProductQuery : IRequest<IEnumerable<GetAllProductResponse>> { }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<GetAllProductResponse>>
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Model.Product> productList = _productRepository.GetAll();

            return _mapper.Map<IEnumerable<GetAllProductResponse>>(productList);
        }
    }
}
