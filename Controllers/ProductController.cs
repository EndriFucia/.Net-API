using System.Reflection;
using System.Net;
using System.IO;
using System;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers
{
    [ApiController]

    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAllProducts()
        {
            return Ok(_mapper.Map<IEnumerable<ProductsReadDTO>>(_repo.GetAllProducts()));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult GetProductById(int id)
        {
            return Ok(_mapper.Map<ProductsReadDTO>(_repo.GetProductById(id)));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductsWriteDTO productsWriteDTO)
        {
            Console.WriteLine(productsWriteDTO.ToString());
            // map the product and create a unique file name
            var product = _mapper.Map<Product>(productsWriteDTO);
            product.Image = Guid.NewGuid().ToString() + "_" + productsWriteDTO.Image;

            //save the image file
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", product.Image);
            await System.IO.File.WriteAllBytesAsync(fullPath, productsWriteDTO.File);

            //Save data to DbContext
            _repo.AddProduct(product);
            _repo.SaveChanges();
            return CreatedAtRoute(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductAsync(int id, ProductsUpdateDTO productsUpdateDTO)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            String imgNameToDelte = product.Image.Split("http://10.0.2.2:5067/Images/")[0];
            String filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/");

            //Delet image
            System.IO.File.Delete(Path.Combine(filePath, imgNameToDelte));
            Console.WriteLine("File deleted.");

            //save the image file
            productsUpdateDTO.Image = Guid.NewGuid().ToString() + "_" + productsUpdateDTO.Image;
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", productsUpdateDTO.Image);
            await System.IO.File.WriteAllBytesAsync(fullPath, productsUpdateDTO.File);
            Console.WriteLine("File updated!.");


            _mapper.Map(productsUpdateDTO, product);
            _repo.UpdateProduct(productsUpdateDTO);
            _repo.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            String imgNameToDelte = product.Image.Split("http://10.0.2.2:5067/Images/")[0];
            String filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/");

            //Delet image
            System.IO.File.Delete(Path.Combine(filePath, product.Image));
            if (!System.IO.File.Exists(Path.Combine(filePath, product.Image)))
            {
                Console.WriteLine("File deleted.");

                _repo.DeleteProduct(product);
                _repo.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}