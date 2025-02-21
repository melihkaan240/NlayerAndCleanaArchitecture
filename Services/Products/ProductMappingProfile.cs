using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;
using AutoMapper;
using App.Services.Products.Create;
using Microsoft.AspNetCore.Routing.Constraints;
using App.Services.Products.Update;

namespace App.Services.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // CreateProductRequest aslında bir dto dur isminde dto yazmasına gerek yoktur request yada response olanlar dtodur
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductRequest, Product>().ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Product>().ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        }
    }
}
