using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Models;

namespace Mapping
{
    public class CustomResolver : IValueResolver<Product, ProductsReadDTO, string>
    {
        public string Resolve(Product source, ProductsReadDTO destination, string destMember, ResolutionContext context)
        {
            return "http://10.0.2.2:5067/Images/" + source.Image;
        }
    }
}