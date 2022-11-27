using BaseProject.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Strategy.DesignPattern.WebApp.Entities;

namespace BaseProject.Identity.Contexts
{
    public class AppIdentityDbContext:IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
