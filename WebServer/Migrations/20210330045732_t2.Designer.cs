﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebServer.Data;

namespace WebServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210330045732_t2")]
    partial class t2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebServer.Models.Category.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsHide")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNum")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2021, 3, 30, 13, 57, 31, 767, DateTimeKind.Local).AddTicks(4539),
                            IsHide = false,
                            Name = "상품",
                            OrderNum = 0,
                            Path = "/product"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2021, 3, 30, 13, 57, 31, 769, DateTimeKind.Local).AddTicks(5295),
                            IsHide = false,
                            Name = "PLACE",
                            OrderNum = 1,
                            Path = "/place"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2021, 3, 30, 13, 57, 31, 769, DateTimeKind.Local).AddTicks(5368),
                            IsHide = false,
                            Name = "게시판",
                            OrderNum = 2,
                            Path = "/board"
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2021, 3, 30, 13, 57, 31, 769, DateTimeKind.Local).AddTicks(5371),
                            IsHide = false,
                            Name = "공지사항",
                            OrderNum = 3,
                            Path = "/notice"
                        });
                });

            modelBuilder.Entity("WebServer.Models.Notes.NoteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("ReadCnt")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WebServer.Models.Notices.NoticeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ReadCnt")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notice");
                });

            modelBuilder.Entity("WebServer.Models.Places.PlaceImageInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageMemo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InfoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PlaceImageInfo");
                });

            modelBuilder.Entity("WebServer.Models.Places.PlaceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KeepCount")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("MainImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlaceInfo");
                });

            modelBuilder.Entity("WebServer.Models.Places.PlaceKeep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("PlaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PlaceKeep");
                });

            modelBuilder.Entity("WebServer.Models.Popup.PopupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ended")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHide")
                        .HasColumnType("bit");

                    b.Property<bool>("IsText")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Popups");
                });

            modelBuilder.Entity("WebServer.Models.Product.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSoldOut")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ReadCnt")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebServer.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "User1",
                            FirstName = "lee",
                            LastName = "jin",
                            Password = "Password1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "User2",
                            FirstName = "lee",
                            LastName = "young",
                            Password = "Password2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
