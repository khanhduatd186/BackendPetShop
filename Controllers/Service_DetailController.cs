using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Service_DetailController : ControllerBase
    {
        private readonly IService_DetailRepository _bookRepo;

        public Service_DetailController(IService_DetailRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllService_Detail()
        {
            try
            {
                return Ok(await _bookRepo.GetAllService_Detail());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("/api/services/{IdService}/Times/{IdTime}")]
        public async Task<IActionResult> GetService_DetailById([FromRoute] int IdService, [FromRoute] int IdTime)
        {
            var Service_Detail = await _bookRepo.GetService_DetaillById(IdService,IdTime);
            return Service_Detail == null ? NotFound() : Ok(Service_Detail);
        }
        [HttpGet("{IdService}")]
        public async Task<IActionResult> GetListById([FromRoute] int IdService)
        {
            var Service_Detail = await _bookRepo.GetListById(IdService);
            return Service_Detail == null ? NotFound() : Ok(Service_Detail);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewService_Detail(Service_DetailModel Service_DetailModel)
        {
            try
            {
                var newService_Detail = await _bookRepo.AddService_Detail(Service_DetailModel);

                return newService_Detail == null ? NotFound() : Ok(newService_Detail);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("/api/Services/{idService}/Times/{idTime}")]


        public async Task<IActionResult> UpdateService_Detail([FromRoute] int idService, [FromRoute] int idTime, [FromBody] Service_DetailModel Service_DetailModel)
        {
            try
            {
                await _bookRepo.UpdateService_Detail(idService, idTime, Service_DetailModel);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteService_Detail([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteService_Detail(id);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("/api/Services/{idService}/Times/{idTime}")]


        public async Task<IActionResult> DeleteService_DetailById([FromRoute] int idService, [FromRoute] int idTime)
        {
            try
            {
                await _bookRepo.DeleteService_DetailById(idService,idTime);
                return Ok(true);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}

