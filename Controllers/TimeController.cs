using ApiPetShop.Interface;
using ApiPetShop.Models;
using ApiPetShop.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeRepository _bookRepo;

        public TimeController(ITimeRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        //[Authorize(Roles = StaticUserRoles.CUTOMER)]
        public async Task<IActionResult> GetAllTime()
        {
            try
            {
                return Ok(await _bookRepo.GetAllTimeAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeById(int id)
        {
            var Time = await _bookRepo.GetTimeAsync(id);
            return Time == null ? NotFound() : Ok(Time);
        }
        [HttpPost]
         
        public async Task<IActionResult> AddNewTime(TimeModel TimeModel)
        {
            try
            {
                var newBookId = await _bookRepo.AddTimeAsync(TimeModel);
                var Time = await _bookRepo.GetTimeAsync(newBookId);
                return Time == null ? NotFound() : Ok(Time);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
         
        public async Task<IActionResult> UpdateTime([FromRoute] int id, [FromBody] TimeModel TimeModel)
        {
            try
            {
                await _bookRepo.UpdateTimeAsync(id, TimeModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
         
        public async Task<IActionResult> DeleteTime([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteTimeAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
