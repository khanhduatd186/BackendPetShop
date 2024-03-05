using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPetShop.Repositories
{
    public class Product_CartRepository:IProduct_CartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public Product_CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<Product_CartModel>> GetAllProduct_Cart()
        {
            var Product_Carts = await _context.product_Carts.ToListAsync();
            return _mapper.Map<List<Product_CartModel>>(Product_Carts);
        }
        public async Task<Product_CartModel> GetProduct_CartById(string IdUser, int IdProduct)
        {
            var Product_Carts = await _context.product_Carts.FirstOrDefaultAsync(p => p.IdUser == IdUser && p.IdProduct == IdProduct);
            return _mapper.Map<Product_CartModel>(Product_Carts);
        }
        public async Task<List<Product_CartModel>> GetProduct_CartlByIdUser(string IdUser)
        {

            var product_Cartss = await _context.product_Carts.Where(p => p.IdUser ==IdUser).ToListAsync();
            return _mapper.Map<List<Product_CartModel>>(product_Cartss);
        }

        public async Task<Product_CartModel> AddProduct_Cart(Product_CartModel Product_Cart)
        {
            var product_Cart = _mapper.Map<Product_Cart>(Product_Cart);
            _context.product_Carts.Add(product_Cart);
            await _context.SaveChangesAsync();
            return _mapper.Map<Product_CartModel>(product_Cart); ;
        }

        public async Task<bool> UpdateProduct_Cart(string IdUser, int ProductId, Product_CartModel product_Cart)
        {
            if (product_Cart.IdUser == IdUser && product_Cart.IdProduct == ProductId)
            {
                var existingProduct_Cart = _mapper.Map<Product_Cart>(product_Cart);
                _context.Update(existingProduct_Cart);
                await _context.SaveChangesAsync();
                return true;
                //await _context.product_Carts.FirstOrDefaultAsync(b => b.IdCart == CartId && b.IdProduct == ProductId);
            }
            return false;
        }

        public async Task<bool> DeleteProduct_Cart(string IdUser)
        {
            var product_Cart = await _context.product_Carts.Where(b => b.IdUser == IdUser).ToListAsync();

            if (product_Cart == null)
            {
                return false;
            }

            _context.product_Carts.RemoveRange(product_Cart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
