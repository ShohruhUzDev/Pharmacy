using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Api.Extensions;
using Pharmacy.Api.Helpers;
using Pharmacy.Api.Middlewares;
using Pharmacy.Data.Contexts;
using Pharmacy.Data.IRepositories;
using Pharmacy.Data.Repositories;
using Pharmacy.Domain.Enums;
using Pharmacy.Service.Interfaces;
using Pharmacy.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConn");
    options.InstanceName = "GamesCatalog_";
});

builder.Services.AddControllers();

builder.Services.AddCors(option =>
{
    option.AddPolicy("MyPolicy", config =>
    {
        config.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<PharmacyDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection")));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.User)));

    options.AddPolicy("UserPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.User)));

    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin)));
});

builder.Services.AddEndpointsApiExplorer();


builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen();// Add custom services

// unit if work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IMedicineOrderService, MedicineOrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(actionModelConvention: new RouteTokenTransformerConvention(
                                 new ConfigureApiUrlName()));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("MyPolicy");
app.UseSwagger();

app.UseSwaggerUI(config => config.SwaggerEndpoint(
    url: "/swagger/v1/swagger.json",
    name: "Pharmacy.API v1"));




app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
