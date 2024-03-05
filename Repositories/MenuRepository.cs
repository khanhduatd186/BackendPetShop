using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ApiPetShop.Repositories
{
    public class MenuRepository: IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MenuRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddMenuAsync(MenuModel model)
        {
            var newMenu = _mapper.Map<Menu>(model);
            _context.menus.Add(newMenu);
            await _context.SaveChangesAsync();
            return newMenu.Id;
        }

        public async Task DeleteMenuAsync(int id)
        {
            var deleteMenu = _context.menus.SingleOrDefault(p => p.Id == id);
            if (deleteMenu != null)
            {
                _context.menus.Remove(deleteMenu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MenuModel>> GetAllMenuAsync()
        {
            var menus = await _context.menus.ToListAsync();
            return _mapper.Map<List<MenuModel>>(menus);
        }

        public async Task<MenuModel> GetMenuAsync(int id)
        {
            var Menu = await _context.menus.FindAsync(id);
            return _mapper.Map<MenuModel>(Menu);
        }

        public async Task UpdateMenuAsync(int id, MenuModel MenuModel)
        {
            if (id == MenuModel.Id)
            {
                var updateMenu = _mapper.Map<Menu>(MenuModel);
                _context.menus.Update(updateMenu);
                await _context.SaveChangesAsync();
            }
        }
    }
}
