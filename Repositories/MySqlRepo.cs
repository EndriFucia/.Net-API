using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Contexts;
using DTO;
using AutoMapper;

namespace Repositories
{
    public class MySqlRepo : IProductRepo
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;
        public List<Product> prodList { get; set; } = new();

        public MySqlRepo()
        {
        }

        public MySqlRepo(ProductsContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            // var products = _mapper.Map<IEnumerable<ProductsReadDTO>>(_context.Products);
            // foreach (ProductsReadDTO product in products)
            // {
            //     product.ImageBytes = File.ReadAllBytes(product.Image);
            // }
            return _context.Products;
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault<Product>(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }


        public void UpdateProduct(Product product)
        {
            //_context.Products.Update(product);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}