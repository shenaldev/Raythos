using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raythos.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ContactNo", "CreatedAt", "Email", "FName", "IsAdmin", "LName", "Password", "UpdatedAt" },
                values: new object[] { 1L, "1234567890", new DateTime(2023, 12, 6, 1, 10, 56, 202, DateTimeKind.Local).AddTicks(9225), "admin@system.com", "Admin", true, "System", "admin", new DateTime(2023, 12, 6, 1, 10, 56, 202, DateTimeKind.Local).AddTicks(9236) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
