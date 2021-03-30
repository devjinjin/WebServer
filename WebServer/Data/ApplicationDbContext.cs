using Microsoft.EntityFrameworkCore;
using System;
using WebServer.Models.Category;
using WebServer.Models.Notes;
using WebServer.Models.Notices;
using WebServer.Models.Popup;
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

            builder
              .Entity<CategoryModel>()
              .HasData(
                  new CategoryModel { Id = 1,Created = DateTime.Now, IsHide = false, Name = "상품", OrderNum = 0, Path = "/product" },
                  new CategoryModel { Id = 2, Created = DateTime.Now, IsHide = false, Name = "PLACE", OrderNum = 1, Path = "/place" },
                  new CategoryModel { Id = 3, Created = DateTime.Now, IsHide = false, Name = "게시판", OrderNum = 2, Path = "/board" },
                  new CategoryModel { Id = 4, Created = DateTime.Now, IsHide = false, Name = "공지사항", OrderNum = 3, Path = "/notice" }
              );
        }
        public DbSet<User> Users { get; set; }

        public DbSet<NoticeModel> Notice { get; set; }

        public DbSet<NoteModel> Notes { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<PopupModel> Popups { get; set; }

        /// <summary>
        /// Place 정보에 대한 객체들
        /// </summary>
        public DbSet<WebServer.Models.Places.PlaceInfo> PlaceInfo { get; set; }

        public DbSet<WebServer.Models.Places.PlaceImageInfo> PlaceImageInfo { get; set; }

        public DbSet<WebServer.Models.Places.PlaceKeep> PlaceKeep { get; set; }
    }
}
