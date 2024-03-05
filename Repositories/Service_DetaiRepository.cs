using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ApiPetShop.Repositories
{
    public class Service_DetailRepository : IService_DetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public Service_DetailRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<Service_DetailModel>> GetAllService_Detail()
        {
            var Service_Details = await _context.Service_Details.ToListAsync();
            return _mapper.Map<List<Service_DetailModel>>(Service_Details);
        }

        public async Task<Service_DetailModel> GetService_DetaillById(int IdService, int TimeId)
        {

            var Service_Detailss = await _context.Service_Details.FirstOrDefaultAsync(p => p.IdService == IdService && p.IdTime == TimeId);
            return _mapper.Map<Service_DetailModel>(Service_Detailss);
        }
        public async Task<List<Service_DetailModel>> GetListById(int IdService)
        {
            var Service_Detailss = await _context.Service_Details.Where(p => p.IdService == IdService).ToListAsync();
            return _mapper.Map<List<Service_DetailModel>>(Service_Detailss);
        }

        public async Task<Service_DetailModel> AddService_Detail(Service_DetailModel Service_Detail)
        {
         
            var service_Detail = _mapper.Map<Service_Detail>(Service_Detail);
            _context.Service_Details.Add(service_Detail);
            await _context.SaveChangesAsync();
            return _mapper.Map<Service_DetailModel>(service_Detail); ;
        }

        public async Task<bool> UpdateService_Detail(int IdService, int TimeId, Service_DetailModel Service_Detail)
        {
            if (Service_Detail.IdService == IdService && Service_Detail.IdTime == TimeId)
            {
                var existingService_Detail = _mapper.Map<Service_Detail>(Service_Detail);
                _context.Update(existingService_Detail);
                await _context.SaveChangesAsync();
                return true;
                //await _context.Service_Details.FirstOrDefaultAsync(b => b.ServiceId == BillId && b.IdProduct == TimeId);
            }
            return false;
        }

        public async Task<bool> DeleteService_Detail(int IdService)
        {
            var Service_Detail = await _context.Service_Details.Where(b => b.IdService == IdService).ToListAsync();

            if (Service_Detail == null)
            {
                return false;
            }

            _context.Service_Details.RemoveRange(Service_Detail);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteService_DetailById(int IdService, int IdTime)
        {
            var Service_Detail = await _context.Service_Details.Where(b => b.IdService == IdService && b.IdTime==IdTime).ToListAsync();

            if (Service_Detail == null)
            {
                return false;
            }

            _context.Service_Details.RemoveRange(Service_Detail);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
