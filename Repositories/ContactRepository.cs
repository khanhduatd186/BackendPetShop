using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class ContactRepository: IContactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddContactAsync(ContactModel model)
        {
            var newContact = _mapper.Map<Contact>(model);
            _context.contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return newContact.Id;
        }

        public async Task DeleteContactAsync(int id)
        {
            var deleteContact = _context.contacts.SingleOrDefault(p => p.Id == id);
            if (deleteContact != null)
            {
                _context.contacts.Remove(deleteContact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ContactModel>> GetAllContactAsync()
        {
            var contacts = await _context.contacts.ToListAsync();
            return _mapper.Map<List<ContactModel>>(contacts);
        }

        public async Task<ContactModel> GetContactAsync(int id)
        {
            var Contact = await _context.contacts.FindAsync(id);
            return _mapper.Map<ContactModel>(Contact);
        }

        public async Task UpdateContactAsync(int id, ContactModel ContactModel)
        {
            if (id == ContactModel.Id)
            {
                var updateContact = _mapper.Map<Contact>(ContactModel);
                _context.contacts.Update(updateContact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
