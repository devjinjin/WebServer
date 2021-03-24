using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;
using WebServer.Models.Notes;
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
                new User { Id = 1, Email = "User1", Password = "Password1", FirstName = "lee", LastName = "jin" },
                new User { Id = 2, Email = "User2", Password = "Password2", FirstName = "lee", LastName = "young" }
                );
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Notice> Notice { get; set; }

        public DbSet<Note> Notes { get; set; }
    }
}
