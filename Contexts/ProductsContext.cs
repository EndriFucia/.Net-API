using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Contexts
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> ListProducts { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {

        }
    }
}