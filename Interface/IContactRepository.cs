using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IContactRepository
    {
        public Task<List<ContactModel>> GetAllContactAsync();
        public Task<ContactModel> GetContactAsync(int id);
        public Task<int> AddContactAsync(ContactModel model);
        public Task UpdateContactAsync(int id, ContactModel ContactModel);
        public Task DeleteContactAsync(int id);
    }
}
