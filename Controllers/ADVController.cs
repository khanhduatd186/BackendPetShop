using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADVController : ControllerBase
    {
        private readonly IADVRepository _bookRepo;

        public ADVController(IADVRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllADV()
        {
            try
            {
                return Ok(await _bookRepo.GetAllADVAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetADVById(int id)
        {
            var ADV = await _bookRepo.GetADVAsync(id);
            return ADV == null ? NotFound() : Ok(ADV);
        }
        [HttpPost]
         

        public async Task<IActionResult> AddNewADV(ADVModel ADVModel)
        {
            try
            {
                var newBookId = await _bookRepo.AddADVAsync(ADVModel);
                var ADV = await _bookRepo.GetADVAsync(newBookId);
                return ADV == null ? NotFound() : Ok(ADV);
            }
            catch
            {
                return BadRequest();
            }

        }
         
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateADV([FromRoute] int id, [FromBody] ADVModel ADVModel)
        {
            try
            {
                await _bookRepo.UpdateADVAsync(id, ADVModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
         

        public async Task<IActionResult> DeleteADV([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteADVAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
