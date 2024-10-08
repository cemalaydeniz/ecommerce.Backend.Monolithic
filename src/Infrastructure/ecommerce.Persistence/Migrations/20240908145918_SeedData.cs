﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /**
         * The IDs must be generated by hand for the seed data. They can be set manually down below if needed.
         */

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "SoftDeletedDate" },
                values: new object[,]
                {
                    { new Guid("1b9ea709-422f-4f63-b92c-e5d687bfefd5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "admin", null },
                    { new Guid("3c05d569-4f14-4b71-90b1-9c29fe919a22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "seller", null },
                    { new Guid("a4726687-a484-47be-830c-77da7d89e798"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "super admin", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailCount", "CreatedDate", "Email", "IsDeleted", "IsEmailConfirmed", "IsLockoutEnabled", "IsPhoneNumberConfirmed", "IsTwoFactorEnabled", "LockoutEndDate", "PasswordHash", "PhoneNumber", "SecurityStamp", "SoftDeletedDate", "UpdatedDate" },
                values: new object[] { new Guid("ce93dd1a-06e7-4ea3-aa13-93065d447264"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "placeholder@example.com", false, true, false, false, false, null, "placeholder", null, new Guid("767449dc-581c-4dc1-b9bb-8e1d5c05e027"), null, null });

            migrationBuilder.InsertData(
                table: "UserUserRole",
                columns: new[] { "UserRolesId", "UsersId" },
                values: new object[] { new Guid("a4726687-a484-47be-830c-77da7d89e798"), new Guid("ce93dd1a-06e7-4ea3-aa13-93065d447264") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b9ea709-422f-4f63-b92c-e5d687bfefd5"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("3c05d569-4f14-4b71-90b1-9c29fe919a22"));

            migrationBuilder.DeleteData(
                table: "UserUserRole",
                keyColumns: new[] { "UserRolesId", "UsersId" },
                keyValues: new object[] { new Guid("a4726687-a484-47be-830c-77da7d89e798"), new Guid("ce93dd1a-06e7-4ea3-aa13-93065d447264") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4726687-a484-47be-830c-77da7d89e798"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ce93dd1a-06e7-4ea3-aa13-93065d447264"));
        }
    }
}
