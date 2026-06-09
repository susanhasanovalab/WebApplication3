using Microsoft.EntityFrameworkCore;

namespace WebApplication3.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
    }
}
