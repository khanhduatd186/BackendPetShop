using ApiPetShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiPetShop.Interface
{
    public interface IUserRepository
    {
        public Task<AuthModel> SigUpAsync(SignUpModel registerDto);
        public Task<AuthModel> SigInAsync(SignInModel userModel);
        public Task<List<SignUpModel>> GetAllUser();
        public Task<SignUpModel> GetUserByEmail(string email);
        public Task<SignUpModel> GetUserById(string id);
        public Task<AuthModel> MakeAdminAsync(UpdatePermissionModel updatePermissionDto);
        public  Task<AuthModel> MakeCutomerAsync(UpdatePermissionModel updatePermissionDto);
        public Task<AuthModel> SeedRolesAsync();
        public string GenerateNewJsonWebToken(List<Claim> claims);
    }   
}
