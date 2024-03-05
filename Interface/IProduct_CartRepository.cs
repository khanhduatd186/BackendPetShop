using ApiPetShop.Models;

namespace ApiPetShop.Interface
{
    public interface IProduct_CartRepository
    {
        public Task<List<Product_CartModel>> GetAllProduct_Cart();
        public Task<Product_CartModel> GetProduct_CartById(string IdUser, int IdProduct);
        public Task<List<Product_CartModel>> GetProduct_CartlByIdUser(string IdUser);
        public Task<Product_CartModel> AddProduct_Cart(Product_CartModel Product_Cart);
        public Task<bool> UpdateProduct_Cart(string IdUser, int ProductId, Product_CartModel Product_CartModel);
        public Task<bool> DeleteProduct_Cart(string IdUser);
    }
}
