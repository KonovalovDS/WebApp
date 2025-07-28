using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountPrice", "IsPriceIncluded", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Описание 1", 0m, false, "Товар 1", 100m, 10 },
                    { 2, "Описание 2", 0m, false, "Товар 2", 200m, 20 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashPassword", "IsAdmin", "Username" },
                values: new object[] { new Guid("9e0b5bdc-073f-448c-9622-3f7bd2ba2c0b"), "admin@gmail.com", "хэш_пароля", true, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9e0b5bdc-073f-448c-9622-3f7bd2ba2c0b"));
        }
    }
}
