using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9e0b5bdc-073f-448c-9622-3f7bd2ba2c0b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashPassword", "IsAdmin", "Username" },
                values: new object[] { new Guid("7cca647a-288b-4d85-9c81-7b33dde781c7"), "admin@gmail.com", "хэш_пароля", true, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7cca647a-288b-4d85-9c81-7b33dde781c7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "HashPassword", "IsAdmin", "Username" },
                values: new object[] { new Guid("9e0b5bdc-073f-448c-9622-3f7bd2ba2c0b"), "admin@gmail.com", "хэш_пароля", true, "admin" });
        }
    }
}
