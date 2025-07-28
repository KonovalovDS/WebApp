using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7cca647a-288b-4d85-9c81-7b33dde781c7"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Quantity" },
                values: new object[] { "Seeded description", "Test Product", 9.99m, 20 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Quantity" },
                values: new object[] { "Описание 1", "Товар 1", 100m, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "DiscountPrice", "IsPriceIncluded", "Name", "Price", "Quantity" },
                values: new object[] { 2, "Описание 2", 0m, false, "Товар 2", 200m, 20 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashPassword", "IsAdmin", "Username" },
                values: new object[] { new Guid("7cca647a-288b-4d85-9c81-7b33dde781c7"), "admin@gmail.com", "хэш_пароля", true, "admin" });
        }
    }
}
