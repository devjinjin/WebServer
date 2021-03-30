using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServer.Migrations
{
    public partial class t1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "IsHide", "Name", "OrderNum", "Path" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 30, 13, 9, 51, 959, DateTimeKind.Local).AddTicks(1606), false, "상품", 0, "/product" },
                    { 2, new DateTime(2021, 3, 30, 13, 9, 51, 960, DateTimeKind.Local).AddTicks(9254), false, "PLACE", 1, "/place" },
                    { 3, new DateTime(2021, 3, 30, 13, 9, 51, 960, DateTimeKind.Local).AddTicks(9309), false, "게시판", 2, "/board" },
                    { 4, new DateTime(2021, 3, 30, 13, 9, 51, 960, DateTimeKind.Local).AddTicks(9312), false, "공지사항", 3, "/notice" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
