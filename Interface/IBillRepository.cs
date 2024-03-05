using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IBillRepository
    {
        public Task<List<BillModel>> GetAllBillAsync();
        public Task<List<BillModel>> GetAllBillById(string IdUser);
        public Task<BillModel> GetBillAsync(int id);
        public Task<BillModel> GetBillAsyncByEmail(double price);
        public Task<int> AddBillAsync(BillModel model);
        public Task UpdateBillAsync(int id, BillModel BillModel);
        public Task DeleteBillAsync(int id);
    }
}
