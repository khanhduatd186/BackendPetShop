using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _bookRepo;

        public CategoryController(ICategoryRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        [Route("GET_ALL")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                return Ok(await _bookRepo.GetAllCategoryAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var Category = await _bookRepo.GetCategoryAsync(id);
            return Category == null ? NotFound() : Ok(Category);
        }
        [HttpPost]
        
        public async Task<IActionResult> AddNewCategory(CategoryModel CategoryModel)
        {
            try
            {
                var newBookId = await _bookRepo.AddCategoryAsync(CategoryModel);
                var Category = await _bookRepo.GetCategoryAsync(newBookId);
                return Category == null ? NotFound() : Ok(Category);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryModel CategoryModel)
        {
            try
            {
                await _bookRepo.UpdateCategoryAsync(id, CategoryModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteCategoryAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
