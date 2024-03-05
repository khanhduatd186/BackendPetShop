using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;


namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _ProductRepo;

        public ProductController(IProductRepository repository)
        {
            _ProductRepo = repository;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                return Ok(await _ProductRepo.GetAllProductAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _ProductRepo.GetProductAsync(id);
            return product == null ? NotFound() : Ok(product);
        }
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> AddNewProductWithImage([FromForm] ProductIFModel productModel)
        {

            try
            {
                var newProductId = await _ProductRepo.AddProductWithImageAsync(productModel);
                var product = await _ProductRepo.GetProductAsync(newProductId);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]

        public async Task<IActionResult> AddNewProduct(ProductModel productModel)
        {
            try
            {
                var newProductId = await _ProductRepo.AddProductAsync(productModel);
                var product = await _ProductRepo.GetProductAsync(newProductId);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]


        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductModel productModel)
        {
            try
            {
                await _ProductRepo.UpdateProductAsync(id, productModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut]
        [Route("updateloadFile/{id}")]
        public async Task<IActionResult> UpdateProductWithImage(int id, [FromForm] ProductIFModel productModel)
        {

            try
            {
                await _ProductRepo.UpdateProductWithImageAsync(id, productModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        


        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                await _ProductRepo.DeleteProductAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
