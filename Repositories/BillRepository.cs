using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BillRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddBillAsync(BillModel model)
        {
            var newBill = _mapper.Map<Bill>(model);
            _context.Bills.Add(newBill);
            await _context.SaveChangesAsync();
            return newBill.Id;
        }

        public async Task DeleteBillAsync(int id)
        {
            var deleteBill = _context.Bills.SingleOrDefault(p => p.Id == id);
            if (deleteBill != null)
            {
                _context.Bills.Remove(deleteBill);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BillModel>> GetAllBillAsync()
        {
            var Bills = await _context.Bills.ToListAsync();
            return _mapper.Map<List<BillModel>>(Bills);
        }
        public async Task<List<BillModel>> GetAllBillById(string IdUser)
        {
            var Bills = await _context.Bills.Where(b => b.IdUser == IdUser).ToListAsync();
            return _mapper.Map<List<BillModel>>(Bills);
        }

        public async Task<BillModel> GetBillAsync(int id)
        {
            var Bill = await _context.Bills.FindAsync(id);
            return _mapper.Map<BillModel>(Bill);
        }
        public async Task<BillModel> GetBillAsyncByEmail( double price)
        {
            var Bill = await _context.Bills.FirstOrDefaultAsync(b => b.Price == price);
            return _mapper.Map<BillModel>(Bill);

        }

        public async Task UpdateBillAsync(int id, BillModel BillModel)
        {
            if (id == BillModel.Id)
            {
                var updateBill = _mapper.Map<Bill>(BillModel);
                _context.Bills.Update(updateBill);
                await _context.SaveChangesAsync();
            }
        }
    }
}
