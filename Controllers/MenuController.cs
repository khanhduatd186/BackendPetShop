using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _bookRepo;

        public MenuController(IMenuRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenu()
        {
            try
            {
                return Ok(await _bookRepo.GetAllMenuAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var Menu = await _bookRepo.GetMenuAsync(id);
            return Menu == null ? NotFound() : Ok(Menu);
        }
        [HttpPost]
         
        public async Task<IActionResult> AddNewMenu(MenuModel MenuModel)
        {
            try
            {
                var newBookId = await _bookRepo.AddMenuAsync(MenuModel);
                var Menu = await _bookRepo.GetMenuAsync(newBookId);
                return Menu == null ? NotFound() : Ok(Menu);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
         
        public async Task<IActionResult> UpdateMenu([FromRoute] int id, [FromBody] MenuModel MenuModel)
        {
            try
            {
                await _bookRepo.UpdateMenuAsync(id, MenuModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
         
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteMenuAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
