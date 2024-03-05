using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IServiceRepository
    {
        public Task<List<ServiceModel>> GetAllServiceAsync();
        public Task<ServiceModel> GetServiceAsync(int id);
        public Task<int> AddServiceAsync(ServiceModel model);
        public Task<int> AddServiceWithImageAsync(ServiceIFModel model);

        public Task UpdateServiceWithImageAsync(int id,ServiceIFModel model);
        public Task UpdateServiceAsync(int id, ServiceModel ServiceModel);
        public Task DeleteServiceAsync(int id);
    }
}
