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
        private readonly IWebHostEnvironment _env;
        private readonly String _hostUrl = "http://10.0.2.2:5067/";

        public ProductController(IProductRepo repo, IMapper mapper, IWebHostEnvironment env)
        {
            _repo = repo;
            _mapper = mapper;
            _env = env;
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
        public ActionResult AddProduct([FromForm] ProductsWriteDTO productsWriteDTO)
        {
            // map the product and create a unique file name
            var product = _mapper.Map<Product>(productsWriteDTO);
            product.Image = Guid.NewGuid().ToString() + "_" + productsWriteDTO.File.FileName;

            //save the image file
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", product.Image);
            productsWriteDTO.File.CopyTo(new FileStream(fullPath, FileMode.Create));

            //Save data to DbContext
            _repo.AddProduct(product);
            _repo.SaveChanges();
            return CreatedAtRoute(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromForm] ProductsUpdateDTO productsUpdateDTO)
        {
            var product = _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            String[] strList = product.Image.Split("http://10.0.2.2:5067/Images/");
            String filePath = _env.WebRootPath + "\\Images\\";

            //Delet image
            if (System.IO.File.Exists(Path.Combine(filePath, strList[0])))
            {
                // If file found, delete it    
                System.IO.File.Delete(Path.Combine(filePath, strList[0]));
                Console.WriteLine("File deleted.");
            }

            //Add the new image
            productsUpdateDTO.Image = Guid.NewGuid().ToString() + "_" + productsUpdateDTO.File.FileName;

            //save the image file
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", productsUpdateDTO.Image);
            productsUpdateDTO.File.CopyTo(new FileStream(fullPath, FileMode.Create));
            Console.WriteLine("File updated!.");


            _mapper.Map(productsUpdateDTO, product);
            _repo.UpdateProduct(productsUpdateDTO);
            _repo.SaveChanges();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var productToDelete = _repo.GetProductById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            _repo.DeleteProduct(productToDelete);
            _repo.SaveChanges();
            return Ok("Product deleted!");
        }
    }
}