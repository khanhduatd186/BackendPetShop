using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class Service_BillRepository:IService_BillRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public Service_BillRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<Service_BillModel>> GetAllService_Bill()
        {
            var service_Bills = await _context.Service_Bills.ToListAsync();
            return _mapper.Map<List<Service_BillModel>>(service_Bills);
        }

        public async Task<List<Service_BillModel>> GetService_BilllById(int Service_BillId, int ServiceId)
        {

            var service_Billss = await _context.Service_Bills.Where(p => p.IdBill == Service_BillId && p.IdService == ServiceId).ToListAsync();
            return _mapper.Map<List<Service_BillModel>>(service_Billss);
        }
        public async Task<List<Service_BillModel>> GetService_BilllByIdBill(int BillId)
        {
            var service_Bills = await _context.Service_Bills.Where(p => p.IdBill == BillId).ToListAsync();
            return _mapper.Map<List<Service_BillModel>>(service_Bills);
        }
        public async Task<Service_BillModel> AddService_Bill(Service_BillModel Service_Bill)
        {
            var service_Bill = _mapper.Map<Service_Bill>(Service_Bill);
            _context.Service_Bills.Add(service_Bill);
            await _context.SaveChangesAsync();
            return _mapper.Map<Service_BillModel>(Service_Bill); ;
        }

        public async Task<bool> UpdateService_Bill(int BillId, int ServiceId, Service_BillModel Service_Bill)
        {
            if (Service_Bill.IdBill == BillId && Service_Bill.IdService == ServiceId)
            {
                var existingService_Bill = _mapper.Map<Service_Bill>(Service_Bill);
                _context.Update(existingService_Bill);
                await _context.SaveChangesAsync();
                return true;
                //await _context.service_Bills.FirstOrDefaultAsync(b => b.IdBill == BillId && b.IdService == ServiceId);
            }
            return false;
        }

        public async Task<bool> DeleteService_Bill(int Service_BillId)
        {
            var Service_Bill = await _context.Service_Bills.Where(b => b.IdBill == Service_BillId).ToListAsync();

            if (Service_Bill == null)
            {
                return false;
            }

            _context.Service_Bills.RemoveRange(Service_Bill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
