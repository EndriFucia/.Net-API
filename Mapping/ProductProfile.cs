using System.Data;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Models;

namespace Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductsReadDTO>().ForMember(dest => dest.Image, opt => opt.MapFrom<CustomResolver>());
            CreateMap<ProductsWriteDTO, Product>();
            CreateMap<ProductsUpdateDTO, Product>();
        }
    }
}