using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; } = String.Empty;
        public String Description { get; set; } = String.Empty;
        public double Price { get; set; }
        public String Image { get; set; } = String.Empty;
        //public byte[] ImageBytes { get; set; } = Array.Empty<byte>();
    }
}