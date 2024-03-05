using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_BillController : ControllerBase
    {
        private readonly IProduct_BillRepository _bookRepo;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _acRepo;

        public Product_BillController(IProduct_BillRepository repository)
        {
            _bookRepo = repository;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct_Bill()
        {
            try
            {
                return Ok(await _bookRepo.GetAllProduct_Bill());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/api/Bills/{idBill}/Products/{idProduct}")]
        public async Task<IActionResult> GetProduct_BillById(int idBill, int idProduct)
        {
            var Product_Bill = await _bookRepo.GetProduct_BilllById(idBill, idProduct);

            return Product_Bill == null ? NotFound() : Ok(Product_Bill);
        }
        [HttpGet("{idBill}")]
        
        public async Task<IActionResult> GetProduct_BillByIdBill(int idBill)
        {
            var Product_Bill = await _bookRepo.GetProduct_BilllByIdBill(idBill);

            return Product_Bill == null ? NotFound() : Ok(Product_Bill);
        }
        [HttpPost]
         

        public async Task<IActionResult> AddNewProduct_Bill(Product_BillModel Product_BillModel)
        {
            try
            {
                var newProduct_Bill = await _bookRepo.AddProduct_Bill(Product_BillModel);
            
                return newProduct_Bill == null ? NotFound() : Ok(newProduct_Bill);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("/api/Bills/{idBill}/Products/{idProduct}")]
         

        public async Task<IActionResult> UpdateProduct_Bill([FromRoute] int idBill, [FromRoute] int idProduct, [FromBody] Product_BillModel Product_BillModel)
        {
            try
            {
                await _bookRepo.UpdateProduct_Bill(idBill,idProduct, Product_BillModel);
               
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
         

        public async Task<IActionResult> DeleteProduct_Bill([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteProduct_Bill(id);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
