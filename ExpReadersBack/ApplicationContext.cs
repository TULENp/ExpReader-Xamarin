using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace ExpReadersBack
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<UserStats> UserStatistics { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>().HasKey(ub => new { ub.BookId, ub.UserId });
        }
    }
}
