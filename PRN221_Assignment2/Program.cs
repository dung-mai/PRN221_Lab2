using Bussiness.IRepository;
using Bussiness.Mapping;
using Bussiness.Repository;
using DataAccess.DataAccess.Models;
using Microsoft.AspNetCore.SignalR;
using PRN221_Assignment2.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/home", "");
});
builder.Services.AddTransient<IProductRepository, ProductRepository>()
    .AddDbContext<NorthwindContext>(opt => builder.Configuration.GetConnectionString("WebConnectionString"));
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>()
    .AddDbContext<NorthwindContext>(opt => builder.Configuration.GetConnectionString("WebConnectionString"));
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>()
    .AddDbContext<NorthwindContext>(opt => builder.Configuration.GetConnectionString("WebConnectionString"));
builder.Services.AddTransient<IOrderRepository, OrderRepository>()
    .AddDbContext<NorthwindContext>(opt => builder.Configuration.GetConnectionString("WebConnectionString"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSession();

var app = builder.Build();

app.MapGet("/", () => "home");
app.MapHub<SignalRServer>("/signalRHub");
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.Run();
