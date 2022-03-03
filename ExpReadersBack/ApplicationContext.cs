using ExpReader.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpReadersBack
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
    }
}
