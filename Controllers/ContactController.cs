using ApiPetShop.Interface;
using ApiPetShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _bookRepo;

        public ContactController(IContactRepository repository)
        {
            _bookRepo = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            try
            {
                return Ok(await _bookRepo.GetAllContactAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
      
        public async Task<IActionResult> GetContactById(int id)
        {
            var Contact = await _bookRepo.GetContactAsync(id);
            return Contact == null ? NotFound() : Ok(Contact);
        }
        [HttpPost]
         
        public async Task<IActionResult> AddNewContact(ContactModel ContactModel)
        {
            try
            {
                var newBookId = await _bookRepo.AddContactAsync(ContactModel);
                var Contact = await _bookRepo.GetContactAsync(newBookId);
                return Contact == null ? NotFound() : Ok(Contact);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
         
        public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] ContactModel ContactModel)
        {
            try
            {
                await _bookRepo.UpdateContactAsync(id, ContactModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
         
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            try
            {
                await _bookRepo.DeleteContactAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
