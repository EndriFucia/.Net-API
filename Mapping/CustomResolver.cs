using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Models;

namespace Mapping
{
    public class CustomResolver : IValueResolver<Product, ProductsReadDTO, byte[]>
    {
        public byte[] Resolve(Product source, ProductsReadDTO destination, byte[] destMember, ResolutionContext context)
        {
            return File.ReadAllBytes("Images/" + source.Image);
        }
    }
}