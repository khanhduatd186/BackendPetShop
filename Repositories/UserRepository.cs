using ApiPetShop.Data;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using ApiPetShop.OtherObjects;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ApiPetShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IMapper mapper,RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<SignUpModel> GetUserByEmail(string email)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            return _mapper.Map<SignUpModel>(user);
        }
        public async Task<List<SignUpModel>> GetAllUser()
        {
            var list = await userManager.Users.ToListAsync();
            return _mapper.Map<List<SignUpModel>>(list);
        }
        public async Task<SignUpModel> GetUserById(string id)
        {
            

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<SignUpModel>(user);
        }
        public async Task<AuthModel> SigInAsync(SignInModel userModel)
        {
            var user = await userManager.FindByNameAsync(userModel.Email);
            if (user is null)
            {
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = "Invalid Credebtials"
                };
            }
            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, userModel.PassWord);
            if (!isPasswordCorrect)
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = "Invalid Credentials"
                };

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("Name", user.Name),
                new Claim("Phone", user.Phone),
                new Claim("Address", user.Adddress)
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GenerateNewJsonWebToken(authClaims);
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            return new AuthModel()
            {
                IsSucceed = true,
                MessageT = token
            };
        }
        public async Task<AuthModel> MakeAdminAsync(UpdatePermissionModel updatePermissionDto)
        {
            var user = await userManager.FindByNameAsync(updatePermissionDto.Email);

            if (user is null)
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = "Invalid email!!!!!!!!"
                };

            await userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

            return new AuthModel()
            {
                IsSucceed = true,
                MessageT = "User is now an ADMIN"
            };
        }
        public async Task<AuthModel> MakeCutomerAsync(UpdatePermissionModel updatePermissionDto)
        {
            var user = await userManager.FindByNameAsync(updatePermissionDto.Email);

            if (user is null)
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = "Invalid email!!!!!!!!"
                };

            await userManager.AddToRoleAsync(user, StaticUserRoles.CUTOMER);

            return new AuthModel()
            {
                IsSucceed = true,
                MessageT = "User is now an ADMIN"
            };
        }
        public async Task<AuthModel> SigUpAsync(SignUpModel registerDto)
        {
            var isExistsUser = await userManager.FindByNameAsync(registerDto.Email);

            if (isExistsUser != null)
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = "UserName Already Exists"
                };


            ApplicationUser newUser = new ApplicationUser()
            {
                Name = registerDto.Name,
                Phone = registerDto.Phone,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Adddress = registerDto.Adddress,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await userManager.CreateAsync(newUser, registerDto.PassWord);


            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Beacause: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new AuthModel()
                {
                    IsSucceed = false,
                    MessageT = errorString
                };
            }
            await userManager.AddToRoleAsync(newUser, StaticUserRoles.CUTOMER);

            return new AuthModel()
            {
                IsSucceed = true,
                MessageT = "User Created Successfully"
            };
        }
        public async Task<AuthModel> SeedRolesAsync()
        {
           
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isCutomerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.CUTOMER);
            bool isManagerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
            if (isAdminRoleExists && isCutomerRoleExists && isManagerRoleExists)
                return new AuthModel()
                {
                    IsSucceed = true,
                    MessageT = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.CUTOMER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));


            return new AuthModel()
            {
                IsSucceed = true,
                MessageT = "Role Seeding Done Successfully"
            };
        }

        public string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var tokenObject = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
         
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }


    }
}
 