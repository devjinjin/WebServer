using Microsoft.EntityFrameworkCore;
using WebServer.Models;
using WebServer.Models.Users;

namespace WebServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<User>()
                .HasData(
                new User { Id = 1, Username = "User1", Password = "Password1" },
                new User { Id = 2, Username = "User2", Password = "Password2" }
                );
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Notice> Notice { get; set; }

   
    }
}
