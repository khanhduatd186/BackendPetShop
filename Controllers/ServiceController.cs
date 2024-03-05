using ApiPetShop.Interface;
using ApiPetShop.Models;
using ApiPetShop.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;


namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _ServiceRepo;

        public ServiceController(IServiceRepository repository)
        {
            _ServiceRepo = repository;
        }
        [HttpGet]
        [Route("GET_ALL")]
        //[Authorize(Roles =StaticUserRoles.ADMIN)]
        public async Task<IActionResult> GetAllService()
        {
            try
            {
                return Ok(await _ServiceRepo.GetAllServiceAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var Service = await _ServiceRepo.GetServiceAsync(id);
            return Service == null ? NotFound() : Ok(Service);
        }
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> AddNewServiceWithImage([FromForm] ServiceIFModel ServiceModel)
        {

            try
            {
                var newServiceId = await _ServiceRepo.AddServiceWithImageAsync(ServiceModel);
                var Service = await _ServiceRepo.GetServiceAsync(newServiceId);
                return Service == null ? NotFound() : Ok(Service);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]

        public async Task<IActionResult> AddNewService(ServiceModel ServiceModel)
        {
            try
            {
                var newServiceId = await _ServiceRepo.AddServiceAsync(ServiceModel);
                var Service = await _ServiceRepo.GetServiceAsync(newServiceId);
                return Service == null ? NotFound() : Ok(Service);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]


        public async Task<IActionResult> UpdateService([FromRoute] int id, [FromBody] ServiceModel ServiceModel)
        {
            try
            {
                await _ServiceRepo.UpdateServiceAsync(id, ServiceModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut]
        [Route("updateloadFile/{id}")]
        public async Task<IActionResult> UpdateServiceWithImage(int id, [FromForm] ServiceIFModel ServiceModel)
        {

            try
            {
                await _ServiceRepo.UpdateServiceWithImageAsync(id, ServiceModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]



        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            try
            {
                await _ServiceRepo.DeleteServiceAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
