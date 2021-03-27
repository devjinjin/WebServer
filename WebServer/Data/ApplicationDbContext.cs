using Microsoft.EntityFrameworkCore;
using WebServer.Models.Category;
using WebServer.Models.Notes;
using WebServer.Models.Notices;
using WebServer.Models.Product;
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

        public DbSet<NoticeModel> Notice { get; set; }

        public DbSet<NoteModel> Notes { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        /// <summary>
        /// Place 정보에 대한 객체들
        /// </summary>
        public DbSet<WebServer.Models.Places.PlaceInfo> PlaceInfo { get; set; }

        public DbSet<WebServer.Models.Places.PlaceImageInfo> PlaceImageInfo { get; set; }

        public DbSet<WebServer.Models.Places.PlaceKeep> PlaceKeep { get; set; }
    }
}
