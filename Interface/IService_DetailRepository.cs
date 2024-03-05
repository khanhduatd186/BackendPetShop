using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IService_DetailRepository
    {
        public Task<List<Service_DetailModel>> GetAllService_Detail();
        public Task<Service_DetailModel> GetService_DetaillById(int IdService, int IdTime);
        public Task<List<Service_DetailModel>>GetListById(int IdService);
        public Task<Service_DetailModel> AddService_Detail(Service_DetailModel Service_Detail);
        public Task<bool> UpdateService_Detail(int IdTime, int ServiceId, Service_DetailModel Service_DetailModel);
        public Task<bool> DeleteService_Detail(int IdSerive);
        public Task<bool> DeleteService_DetailById(int IdSerive, int IdTime);
    }
}
