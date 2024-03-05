using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ApiPetShop.Repositories
{
    public class Product_BillRepository:IProduct_BillRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public Product_BillRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        
        }

        public async Task<List<Product_BillModel>> GetAllProduct_Bill()
        {
            var Product_Bills = await _context.product_Bills.ToListAsync();
            return _mapper.Map<List<Product_BillModel>>(Product_Bills);
        }

        public async Task<List<Product_BillModel>> GetProduct_BilllById(int Product_BillId , int ProductId )
        {
            
            var product_Billss = await _context.product_Bills.Where(p => p.IdBill == Product_BillId && p.IdProduct == ProductId).ToListAsync();
            return _mapper.Map<List<Product_BillModel>>(product_Billss);
        }
        public async Task<List<Product_BillModel>> GetProduct_BilllByIdBill(int IdBill)
        {
            var product_Bills = await _context.product_Bills.Where(p => p.IdBill == IdBill).ToListAsync();
            return _mapper.Map<List<Product_BillModel>>(product_Bills);
        }

        public async Task<Product_BillModel> AddProduct_Bill(Product_BillModel Product_Bill)
        {
            var product_Bill = _mapper.Map<Product_Bill>(Product_Bill);
            _context.product_Bills.Add(product_Bill);
            await _context.SaveChangesAsync();
            return _mapper.Map<Product_BillModel>(product_Bill); ;
        }

        public async Task<bool> UpdateProduct_Bill(int BillId,int ProductId, Product_BillModel product_Bill)
        {
            if(product_Bill.IdBill == BillId && product_Bill.IdProduct == ProductId)
            {
                var existingProduct_Bill = _mapper.Map<Product_Bill>(product_Bill);
                _context.Update(existingProduct_Bill);
                await _context.SaveChangesAsync();
                return true;
                //await _context.product_Bills.FirstOrDefaultAsync(b => b.IdBill == BillId && b.IdProduct == ProductId);
            }
            return false;
        }

        public async Task<bool> DeleteProduct_Bill(int Product_BillId)
        {
            var product_Bill = await _context.product_Bills.Where(b => b.IdBill == Product_BillId).ToListAsync();

            if (product_Bill == null)
            {
                return false;
            }

            _context.product_Bills.RemoveRange(product_Bill);
            await _context.SaveChangesAsync();
            return true;
        }

     
    }
}
