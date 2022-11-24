using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductsWriteDTO
    {
        public String Name { get; set; } = String.Empty;
        public String Description { get; set; } = String.Empty;
        public double Price { get; set; }
        public string Image { get; set; } = String.Empty;
        public IFormFile File { get; set; }
    }
}