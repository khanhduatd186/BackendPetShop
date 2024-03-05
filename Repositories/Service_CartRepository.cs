using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class Service_CartRepository:IService_CartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public Service_CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<Service_CartModel>> GetAllService_Cart()
        {
            var Service_Carts = await _context.service_Carts.ToListAsync();
            return _mapper.Map<List<Service_CartModel>>(Service_Carts);
        }

        public async Task<List<Service_CartModel>> GetService_CartlByIdUser(string IdUser)
        {

            var Service_Cartss = await _context.service_Carts.Where(p => p.IdUser == IdUser).ToListAsync();
            return _mapper.Map<List<Service_CartModel>>(Service_Cartss);
        }

        public async Task<Service_CartModel> AddService_Cart(Service_CartModel Service_Cart)
        {
            var service_Cart = _mapper.Map<Service_Cart>(Service_Cart);
            _context.service_Carts.Add(service_Cart);
            await _context.SaveChangesAsync();
            return _mapper.Map<Service_CartModel>(Service_Cart); ;
        }

        public async Task<bool> UpdateService_Cart(string IdUser, int ServiceId, Service_CartModel Service_Cart)
        {
            if (Service_Cart.IdUser == IdUser && Service_Cart.IdServie == ServiceId)
            {
                var existingService_Cart = _mapper.Map<Service_Cart>(Service_Cart);
                _context.Update(existingService_Cart);
                await _context.SaveChangesAsync();
                return true;
                //await _context.Service_Carts.FirstOrDefaultAsync(b => b.IdCart == CartId && b.IdService == ServiceId);
            }
            return false;
        }

        public async Task<bool> DeleteService_Cart(string IdUser)
        {
            var Service_Cart = await _context.service_Carts.Where(b => b.IdUser == IdUser).ToListAsync();

            if (Service_Cart == null)
            {
                return false;
            }

            _context.service_Carts.RemoveRange(Service_Cart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
