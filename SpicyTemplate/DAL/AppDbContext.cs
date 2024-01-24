using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpicyTemplate.Models;

namespace SpicyTemplate.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<OurMenu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
