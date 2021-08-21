using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {
            
        }

        public DbSet<City> City { get; set; }
        public DbSet<Region> Region {  get; set; }
        public DbSet<Country> Country { get; set; }
    }
}
