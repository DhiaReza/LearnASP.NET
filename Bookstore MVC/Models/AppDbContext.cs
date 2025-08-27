using Microsoft.EntityFrameworkCore;

namespace Bookstore_MVC.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Book> Book { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    };
}