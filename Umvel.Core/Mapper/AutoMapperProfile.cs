using AutoMapper;
using Umvel.Contracts.DTO.Customer;
using Umvel.Contracts.DTO.Product;
using Umvel.Contracts.DTO.Sale;
using Model = Umvel.Infrastructure.Database.Models;

namespace Umvel.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Model.Customer, RegisterCustomerResponse>().ReverseMap();
            CreateMap<Model.Customer, RegisterCustomerRequest>().ReverseMap();
            CreateMap<Model.Customer, GetCustomerByIdResponse>().ReverseMap();
            CreateMap<Model.Customer, GetAllCustomerResponse>().ReverseMap();

            CreateMap<Model.Product, RegisterProductResponse>().ReverseMap();
            CreateMap<Model.Product, RegisterProductRequest>().ReverseMap();
            CreateMap<Model.Product, GetProductByIdResponse>().ReverseMap();
            CreateMap<Model.Product, GetAllProductResponse>().ReverseMap();

            CreateMap<Model.Sale, RegisterSaleRequest>().ReverseMap();
            CreateMap<Model.Sale, RegisterSaleResponse>().ReverseMap();
            CreateMap<Model.Sale, GetSaleResponse>().ReverseMap();
        }
    }
}
