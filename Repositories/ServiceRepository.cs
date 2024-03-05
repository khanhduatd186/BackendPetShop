using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiPetShop.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        public async Task<int> AddServiceAsync(ServiceModel model)
        {

            var newService = _mapper.Map<Service>(model);
            _context.services.Add(newService);
            await _context.SaveChangesAsync();
            return newService.Id;
        }
        public async Task<int> AddServiceWithImageAsync(ServiceIFModel model)
        {


            var newService = new Service { Id = model.Id, Tittle = model.Tittle, Description = model.Description, Price = model.Price, Isdelete = model.Isdelete };

            if (model.Picture.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", model.Picture.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await model.Picture.CopyToAsync(stream);
                }
                newService.Image = "/img/" + model.Picture.FileName;

            }
            else
            {
                newService.Image = "";
            }
            _context.services.Add(newService);
            await _context.SaveChangesAsync();
            return newService.Id;
        }

        public async Task DeleteServiceAsync(int id)
        {
            var deleteService = _context.services.SingleOrDefault(p => p.Id == id);
            if (deleteService != null)
            {
                _context.services.Remove(deleteService);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ServiceModel>> GetAllServiceAsync()
        {
            var services = await _context.services.ToListAsync();
            return _mapper.Map<List<ServiceModel>>(services);
        }

        public async Task<ServiceModel> GetServiceAsync(int id)
        {
            var Service = await _context.services.FindAsync(id);
            return _mapper.Map<ServiceModel>(Service);
        }

        public async Task UpdateServiceAsync(int id, ServiceModel ServiceModel)
        {
            if (id == ServiceModel.Id)
            {
                var updateService = _mapper.Map<Service>(ServiceModel);
                _context.services.Update(updateService);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateServiceWithImageAsync(int id, ServiceIFModel model)
        {
            var newService = new Service { Id = model.Id, Tittle = model.Tittle, Description = model.Description,  Price = model.Price, Isdelete = model.Isdelete };
            if (id == newService.Id)
            {

                if (model.Picture.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", model.Picture.FileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await model.Picture.CopyToAsync(stream);
                    }
                    newService.Image = "/img/" + model.Picture.FileName;

                }
                else
                {
                    newService.Image = "";
                }
                _context.services.Update(newService);
                await _context.SaveChangesAsync();
            }

        }
    }
}
