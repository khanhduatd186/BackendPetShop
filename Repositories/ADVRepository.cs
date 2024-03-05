using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class ADVRepository: IADVRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ADVRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddADVAsync(ADVModel model)
        {
            var newADV = _mapper.Map<ADV>(model);
            _context.ADVs.Add(newADV);
            await _context.SaveChangesAsync();
            return newADV.Id;
        }

        public async Task DeleteADVAsync(int id)
        {
            var deleteADV = _context.ADVs.SingleOrDefault(p => p.Id == id);
            if (deleteADV != null)
            {
                _context.ADVs.Remove(deleteADV);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ADVModel>> GetAllADVAsync()
        {
            var ADVs = await _context.ADVs.ToListAsync();
            return _mapper.Map<List<ADVModel>>(ADVs);
        }

        public async Task<ADVModel> GetADVAsync(int id)
        {
            var ADV = await _context.ADVs.FindAsync(id);
            return _mapper.Map<ADVModel>(ADV);
        }

        public async Task UpdateADVAsync(int id, ADVModel ADVModel)
        {
            if (id == ADVModel.Id)
            {
                var updateADV = _mapper.Map<ADV>(ADVModel);
                _context.ADVs.Update(updateADV);
                await _context.SaveChangesAsync();
            }
        }
    }
}
