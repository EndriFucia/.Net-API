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

        public Product(int id = 0, String name = "", String description = "", double price = 0, String image = "")
        {
            try
            {
                if (Validator.IsValidProduct(id, name, description, price, image))
                {
                    Id = id;
                    Name = name;
                    Description = description;
                    Price = price;
                    Image = image;
                }
                else
                {
                    throw new Exception("Invalid input for product!");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured : ", e.Message);
            }
        }
    }
}