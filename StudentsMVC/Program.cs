using Microsoft.EntityFrameworkCore;
using MvcProject.Models;
using MvcProject.Services;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopApplication.Mapping;
using ShopApplication.Services;
using ShopInfrastructure.Data;
using ShopInfrastructure.Repositories;
using ShopInfrastructure.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

string? connection =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddScoped<IProductService, ProductService>();

string? shopConnection =
    builder.Configuration.GetConnectionString("ShopSqlServerConnection");

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(shopConnection));

builder.Services.AddAutoMapper(
    _ => { },
    typeof(CategoryProfile).Assembly);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnection =
        builder.Configuration.GetConnectionString("Redis");

    return ConnectionMultiplexer.Connect(redisConnection);
});

builder.Services.AddScoped<ICachingService, RedisCachingService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();