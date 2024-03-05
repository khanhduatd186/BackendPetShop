using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class TimeRepository:ITimeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TimeRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddTimeAsync(TimeModel model)
        {
            var newTime = _mapper.Map<Time>(model);
            _context.Times.Add(newTime);
            await _context.SaveChangesAsync();
            return newTime.Id;
        }

        public async Task DeleteTimeAsync(int id)
        {
            var deleteTime = _context.Times.SingleOrDefault(p => p.Id == id);
            if (deleteTime != null)
            {
                _context.Times.Remove(deleteTime);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TimeModel>> GetAllTimeAsync()
        {
            var Times = await _context.Times.ToListAsync();
            return _mapper.Map<List<TimeModel>>(Times);
        }

        public async Task<TimeModel> GetTimeAsync(int id)
        {
            var Time = await _context.Times.FindAsync(id);
            return _mapper.Map<TimeModel>(Time);
        }

        public async Task UpdateTimeAsync(int id, TimeModel TimeModel)
        {
            if (id == TimeModel.Id)
            {
                var updateTime = _mapper.Map<Time>(TimeModel);
                _context.Times.Update(updateTime);
                await _context.SaveChangesAsync();
            }
        }
    }
}
