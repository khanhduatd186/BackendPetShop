using ApiPetShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPetShop.Interface
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllProductAsync();
        public Task<ProductModel> GetProductAsync(int id);
        public Task<int> AddProductAsync(ProductModel model);
        public Task<int> AddProductWithImageAsync(ProductIFModel model);


        public Task UpdateProductWithImageAsync(int id,ProductIFModel model);
        public Task UpdateProductAsync(int id, ProductModel productModel);
        public Task DeleteProductAsync(int id);
    }
}
