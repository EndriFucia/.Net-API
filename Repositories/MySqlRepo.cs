using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Contexts;

namespace Repositories
{
    public class MySqlRepo : IProductRepo
    {
        private readonly ProductsContext _context;

        public MySqlRepo()
        {
        }

        public MySqlRepo(ProductsContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.ListProducts;
        }

        public Product GetProductById(int id)
        {
            return _context.ListProducts.FirstOrDefault<Product>(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            _context.ListProducts.Add(product);
        }


        public void UpdateProduct(Product product)
        {
            //_context.ListProducts.Update(product);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.ListProducts.Remove(product);
        }
    }
}