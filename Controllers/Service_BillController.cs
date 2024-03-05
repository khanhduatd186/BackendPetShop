using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Service_BillController : ControllerBase
    {
        private readonly IService_BillRepository _bookRepo;

        public Service_BillController(IService_BillRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllService_Bill()
        {
            try
            {
                return Ok(await _bookRepo.GetAllService_Bill());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("/api/Biils/{IdBill}/Services/{IdService}")]
        public async Task<IActionResult> GetService_BillById([FromRoute] int IdBill, [FromRoute] int IdService)
        {
            var Service_Bill = await _bookRepo.GetService_BilllById(IdBill, IdService);
            return Service_Bill == null ? NotFound() : Ok(Service_Bill);
        }
        [HttpGet("{IdBill}")]
        public async Task<IActionResult> GetListById([FromRoute] int IdBill)
        {
            var Service_Bill = await _bookRepo.GetService_BilllByIdBill(IdBill);
            return Service_Bill == null ? NotFound() : Ok(Service_Bill);
        }

        [HttpPost]

        public async Task<IActionResult> AddNewService_Bill(Service_BillModel Service_BillModel)
        {
            try
            {
                var newService_Bill = await _bookRepo.AddService_Bill(Service_BillModel);

                return newService_Bill == null ? NotFound() : Ok(newService_Bill);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("/api/Bills/{idBill}/Service/{idService}")]


        public async Task<IActionResult> UpdateService_Bill([FromRoute] int idBill, [FromRoute] int idService, [FromBody] Service_BillModel Service_BillModel)
        {
            try
            {
                await _bookRepo.UpdateService_Bill(idBill, idService, Service_BillModel);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteService_Bill([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteService_Bill(id);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
