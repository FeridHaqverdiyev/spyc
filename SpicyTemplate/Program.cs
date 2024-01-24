using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.DAL;
using SpicyTemplate.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
		   name: "areas",
		   pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
		 );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
