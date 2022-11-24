using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Models;

namespace Repositories
{
    public interface IProductRepo
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(ProductsUpdateDTO product);
        void DeleteProduct(Product product);
        void SaveChanges();
    }
}