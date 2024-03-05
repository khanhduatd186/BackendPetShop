using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiPetShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbcontext,IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddProductAsync(ProductModel model)
        {
            
            var newProduct = _mapper.Map<Product>(model);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct.Id;
        }
        public async Task<int> AddProductWithImageAsync(ProductIFModel model)
        {


            var newProduct = new Product { Id = model.Id, Tittle = model.Tittle, Description = model.Description, Quantity = model.Quantity, Price = model.Price, CategoryId = model.CategoryId, Isdelete = model.Isdelete };

            if (model.Picture.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", model.Picture.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await model.Picture.CopyToAsync(stream);
                }
                newProduct.Image = "/img/" + model.Picture.FileName;

            }
            else
            {
                newProduct.Image = "";
            }
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return newProduct.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var deleteProduct = _context.Products.SingleOrDefault(p => p.Id == id);
            if(deleteProduct != null)
            {
                _context.Products.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(int id, ProductModel productModel)
        {
            if(id == productModel.Id)
            {
                var updateProduct = _mapper.Map<Product>(productModel);
                _context.Products.Update(updateProduct);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateProductWithImageAsync(int id,ProductIFModel model)
        {
            var newProduct = new Product { Id = model.Id, Tittle = model.Tittle, Description = model.Description, Quantity = model.Quantity, Price = model.Price, CategoryId = model.CategoryId, Isdelete = model.Isdelete };
            if (id == newProduct.Id)
            {
                
                if (model.Picture.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", model.Picture.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await model.Picture.CopyToAsync(stream);
                    }
                    newProduct.Image = "/img/" + model.Picture.FileName;

                }
                else
                {
                    newProduct.Image = "";
                }
                _context.Products.Update(newProduct);
                await _context.SaveChangesAsync();
            }
  
        }
    }
}
