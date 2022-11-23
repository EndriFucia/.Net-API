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
            //return Ok(_repo.GetAllProducts());
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
            //product.Image = Guid.NewGuid().ToString() + "_" + productsWriteDTO.File.FileName;

            //save the image file
            // var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Images/", product.Image);
            // productsWriteDTO.File.CopyTo(new FileStream(fullPath, FileMode.Create));

            //Save data to DbContext
            _repo.AddProduct(product);
            _repo.SaveChanges();
            return CreatedAtRoute(nameof(GetProductById), new { Id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductsUpdateDTO productsUpdateDTO)
        {
            var productToUpdate = _repo.GetProductById(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(productsUpdateDTO, productToUpdate);
            _repo.UpdateProduct(productToUpdate);
            _repo.SaveChanges();
            return Ok(productToUpdate);
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