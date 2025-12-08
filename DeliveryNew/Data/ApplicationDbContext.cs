using Microsoft.EntityFrameworkCore;
using DeliveryNew.Models;

namespace DeliveryNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryItem> DeliveryItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
