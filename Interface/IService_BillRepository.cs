using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IService_BillRepository
    {
        public Task<List<Service_BillModel>> GetAllService_Bill();
        public Task<List<Service_BillModel>> GetService_BilllById(int Service_BillId, int Service);
        public Task<List<Service_BillModel>> GetService_BilllByIdBill(int BillId);
        public Task<Service_BillModel> AddService_Bill(Service_BillModel Service_Bill);
        public Task<bool> UpdateService_Bill(int BillId, int ServiceId, Service_BillModel Service_BillModel);
        public Task<bool> DeleteService_Bill(int Service_BillId);
    }
}
