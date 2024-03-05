using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryModel>> GetAllCategoryAsync();
        public Task<CategoryModel> GetCategoryAsync(int id);
        public Task<int> AddCategoryAsync(CategoryModel model);
        public Task UpdateCategoryAsync(int id, CategoryModel CategoryModel);
        public Task DeleteCategoryAsync(int id);
    }
}
