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
            CreateMap<Product, ProductsReadDTO>();
            CreateMap<ProductsWriteDTO, Product>();
            CreateMap<ProductsUpdateDTO, Product>();
        }
    }
}