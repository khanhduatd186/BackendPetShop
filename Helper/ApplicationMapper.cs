using ApiPetShop.Data;
using ApiPetShop.Models;
using AutoMapper;
namespace ApiPetShop.Helper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<ApplicationUser, SignUpModel>().ReverseMap();
            CreateMap<ApplicationUser, SignInModel>().ReverseMap();
            CreateMap<Bill, BillModel>().ReverseMap();
            CreateMap<Time, TimeModel>().ReverseMap();
            CreateMap<Service, ServiceModel>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<Contact, ContactModel>().ReverseMap();
            CreateMap<ADV, ADVModel>().ReverseMap();
            CreateMap<Product_Bill, Product_BillModel>().ReverseMap();
            CreateMap<Service_Bill, Service_BillModel>().ReverseMap();
            CreateMap<Product_Cart, Product_CartModel>().ReverseMap();
            CreateMap<Service_Cart, Service_CartModel>().ReverseMap();
            CreateMap<Service_Detail, Service_DetailModel>().ReverseMap();
            //CreateMap<Product,ProductIFModel>().ReverseMap();

        }
        
    }
}
