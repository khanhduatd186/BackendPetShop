using ApiPetShop.Data;
using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IProduct_BillRepository
    {
        public Task<List<Product_BillModel>> GetAllProduct_Bill();
        public Task<List<Product_BillModel>>GetProduct_BilllById(int Product_BillId, int ProductId);
        public Task<List<Product_BillModel>> GetProduct_BilllByIdBill(int IdBill);
        public Task<Product_BillModel> AddProduct_Bill(Product_BillModel Product_Bill);
        public Task<bool> UpdateProduct_Bill(int BillId, int ProductId, Product_BillModel Product_BillModel);
        public Task<bool> DeleteProduct_Bill(int Product_BillId);

   
    }
}
