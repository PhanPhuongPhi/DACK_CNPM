using Microsoft.EntityFrameworkCore;
using WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Kết nối DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
