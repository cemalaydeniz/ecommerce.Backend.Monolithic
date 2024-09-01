using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Guid superAdminId = Guid.NewGuid();
            Guid superAdminRoleId = Guid.NewGuid();

            string superAdminEmail = "placeholder@example.com";     // Change this while migrating

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "SoftDeletedDate" },
                values: new object[,]
                {
                    { superAdminRoleId, DateTime.UtcNow, false, "super admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { Guid.NewGuid(), DateTime.UtcNow, false, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { Guid.NewGuid(), DateTime.UtcNow, false, "seller", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailCount", "AccountStatus", "CreatedDate", "Email", "IsDeleted", "IsEmailConfirmed", "IsLockoutEnabled", "IsPhoneNumberConfirmed", "IsTwoFactorEnabled", "LockoutEndDate", "PasswordHash", "PhoneNumber", "SecurityStamp", "SoftDeletedDate", "UpdatedDate" },
                values: new object[] { superAdminId, 0, "Active", DateTime.UtcNow, superAdminEmail, false, true, false, false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "placeholder", null, Guid.NewGuid(), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "UserUserRole",
                columns: new[] { "UsersId", "UserRolesId" },
                values: new object[,]
                {
                    { superAdminId, superAdminRoleId }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("6382846b-fe71-492b-95f1-c42dae9563c3"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("a20a6f58-fba0-4c69-be98-8a9b53939883"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("d54b787c-9e59-4e24-b2a3-5c809800790a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("97ad4696-007b-42b9-8121-75d6538d7463"));
        }
    }
}
