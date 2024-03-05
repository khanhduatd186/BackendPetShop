using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddCategoryAsync(CategoryModel model)
        {
            var newCategory = _mapper.Map<Category>(model);
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory.Id;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var deleteCategory = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (deleteCategory != null)
            {
                _context.Categories.Remove(deleteCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            var Categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(Categories);
        }

        public async Task<CategoryModel> GetCategoryAsync(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryModel>(Category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryModel CategoryModel)
        {
            if (id == CategoryModel.Id)
            {
                var updateCategory = _mapper.Map<Category>(CategoryModel);
                _context.Categories.Update(updateCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
