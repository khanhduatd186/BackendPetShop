using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface ITimeRepository
    {
        public Task<List<TimeModel>> GetAllTimeAsync();
        public Task<TimeModel> GetTimeAsync(int id);
        public Task<int> AddTimeAsync(TimeModel model);
        public Task UpdateTimeAsync(int id, TimeModel TimeModel);
        public Task DeleteTimeAsync(int id);
    }
}
