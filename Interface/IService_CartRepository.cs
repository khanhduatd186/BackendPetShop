using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IService_CartRepository
    {
        public Task<List<Service_CartModel>> GetAllService_Cart();
        public Task<List<Service_CartModel>> GetService_CartlByIdUser(string IdUser);
        public Task<Service_CartModel> AddService_Cart(Service_CartModel Service_Cart);
        public Task<bool> UpdateService_Cart(string IdUser, int ServiceId, Service_CartModel Service_CartModel);
        public Task<bool> DeleteService_Cart(string IdUser);
    }
}
