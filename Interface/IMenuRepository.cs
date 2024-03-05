using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IMenuRepository
    {
        public Task<List<MenuModel>> GetAllMenuAsync();
        public Task<MenuModel> GetMenuAsync(int id);
        public Task<int> AddMenuAsync(MenuModel model);
        public Task UpdateMenuAsync(int id, MenuModel MenuModel);
        public Task DeleteMenuAsync(int id);
    }
}
