using ApiPetShop.Data;
using ApiPetShop.Helper;
using ApiPetShop.Interface;
using ApiPetShop.Models;
using ApiPetShop.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Application.Common.Options;
using Application.Interfaces;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<NotifyGoogleGmailApiOption>(builder.Configuration.GetSection("GoogleAPI:NotifyGmail"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPetShop", Version = "1.0" });
});
//builder.Services.AddSwaggerGen(s =>
//    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Jwt Authorization",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    })

//);
//builder.Services.AddHangfire(configuration => configuration
//    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()); // Bạn có thể sử dụng một loại lưu trữ khác nếu cần

//builder.Services.AddHangfireServer();

builder.Services.AddSwaggerGen(c =>
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type =  ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string []{}
            }
        }
    ));


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetStore"));
});

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Config Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
});


// Add Authentication and JwtBearer
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidateIssuerSigningKey = true,
          
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IADVRepository, ADVRepository>();
builder.Services.AddScoped<IProduct_BillRepository, Product_BillRepository>();
builder.Services.AddScoped<IService_BillRepository, Service_BillRepository>();
builder.Services.AddScoped<IProduct_CartRepository, Product_CartRepository>();
builder.Services.AddScoped<IService_CartRepository, Service_CartRepository>();
builder.Services.AddScoped<IService_DetailRepository, Service_DetailRepository>();
builder.Services.AddHangfire((sp, config) =>
{
    var connect = sp.GetRequiredService<IConfiguration>().GetConnectionString("PetStore");
    config.UseSqlServerStorage(connect);
});
builder.Services.AddHangfireServer();


var app = builder.Build();
//app.MapPost("/security/createToken",
//    [AllowAnonymousAttribute] (SignInModel user) =>
//    {
//        if (user.Email == "adminapi@gmail.com" && user.PassWord == "Hutech@123")
//        {
//            var issuer = builder.Configuration["JWT:ValidIssuer"],;
//            var audience = builder.Configuration["JWT:ValidAudience"];
//            var ket = Encoding.ASCII.GetBytes
//            }

//    });


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard(); // Để quản lý các công việc
app.UseHangfireServer();
RecurringJob.AddOrUpdate<SetAutoCreateSchedule>("SetAutoCreateSchedule", x => x.UpdateAndDelete(), Cron.Daily());
RecurringJob.AddOrUpdate<RemindBookingSchedule>("RemindBookingSchedule", x => x.SendMailRemind(), Cron.Daily());
RecurringJob.AddOrUpdate<RemindBookingSchedule>("RemindBookingSchedule2", x => x.SendMailRemind2(), Cron.Hourly());
RecurringJob.AddOrUpdate<SetAutoCreateSchedule>("CheckBaoHanh", x => x.CheckBaoHanh(), Cron.Daily());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
