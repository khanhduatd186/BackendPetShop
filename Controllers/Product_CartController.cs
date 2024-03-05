using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_CartController : ControllerBase
    {
        private readonly IProduct_CartRepository _bookRepo;

        public Product_CartController(IProduct_CartRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct_Cart()
        {
            try
            {
                return Ok(await _bookRepo.GetAllProduct_Cart());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{IdUser}")]
        public async Task<IActionResult> GetProduct_CartById(string IdUser)
        {
            var Product_Cart = await _bookRepo.GetProduct_CartlByIdUser(IdUser);
            return Product_Cart == null ? NotFound() : Ok(Product_Cart);
        }
        [HttpPost]
         

        public async Task<IActionResult> AddNewProduct_Cart(Product_CartModel Product_CartModel)
        {
            try
            {
                var newProduct_Cart = await _bookRepo.AddProduct_Cart(Product_CartModel);

                return newProduct_Cart == null ? NotFound() : Ok(newProduct_Cart);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("/api/ApplicationUser/{IdUser}/Products/{idProduct}")]
         

        public async Task<IActionResult> UpdateProduct_Cart([FromRoute] string IdUser, [FromRoute] int idProduct, [FromBody] Product_CartModel Product_CartModel)
        {
            try
            {
                await _bookRepo.UpdateProduct_Cart(IdUser, idProduct, Product_CartModel);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{IdUser}")]
         

        public async Task<IActionResult> DeleteProduct_Cart([FromRoute] string IdUser)
        {
            try
            {
                await _bookRepo.DeleteProduct_Cart(IdUser);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
