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

        public MySqlRepo(ProductsContext context, IWebHostEnvironment env)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(int id)
        {
            Product p = new Product();
            var prod = _context.Products.FirstOrDefault<Product>(p => p.Id == id);
            if (prod != null)
            {
                p = prod;
            }

            return p;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }


        public void UpdateProduct(ProductsUpdateDTO product)
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