using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IADVRepository
    {
        public Task<List<ADVModel>> GetAllADVAsync();
        public Task<ADVModel> GetADVAsync(int id);
        public Task<int> AddADVAsync(ADVModel model);
        public Task UpdateADVAsync(int id, ADVModel ADVModel);
        public Task DeleteADVAsync(int id);
    }
}
