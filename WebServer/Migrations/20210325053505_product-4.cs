using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.Migrations
{
    public partial class product4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modified",
                table: "Products",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "created",
                table: "Products",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "Products",
                newName: "modified");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Products",
                newName: "created");
        }
    }
}
