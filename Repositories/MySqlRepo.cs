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
        private readonly IWebHostEnvironment _env;
        public List<Product> prodList { get; set; } = new();

        public MySqlRepo(ProductsContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IEnumerable<Product> GetAllProducts()
        {
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
            String[] strList = product.Image.Split("http://10.0.2.2:5067/Images/");
            String filePath = _env.WebRootPath + "\\Images\\";

            if (File.Exists(Path.Combine(filePath, strList[0])))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(filePath, strList[0]));
                Console.WriteLine("File deleted.");
            }

            _context.Products.Remove(product);
        }
    }
}