using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.Migrations
{
    public partial class Place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceImageInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InfoId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ImageMemo = table.Column<string>(nullable: true),
                    RegistDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceImageInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Discription = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    OpenTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    CloseDay = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PlaceNotice = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PostAddress = table.Column<string>(nullable: true),
                    OriginPrice = table.Column<double>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    HomePage = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    RegistDate = table.Column<DateTime>(nullable: false),
                    KeepCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceKeep",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<int>(nullable: false),
                    RegistDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceKeep", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceImageInfo");

            migrationBuilder.DropTable(
                name: "PlaceInfo");

            migrationBuilder.DropTable(
                name: "PlaceKeep");
        }
    }
}
