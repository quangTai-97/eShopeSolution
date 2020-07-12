using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdenityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "AppRole",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 200);

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "description" },
                values: new object[] { new Guid("3969955c-023e-418f-8568-0546f5296145"), "e428d7c0-69b4-4709-af0f-952d2964ea0a", "admin", "admin", "Administrator role" });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"), 0, "69f193b5-27ae-45d5-b909-ec1b575df150", new DateTime(2020, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "doquangtaia3@gmail.com", true, "Tai", "Do", false, null, "doquangtaia3@gmail.com", "admin", "AQAAAAEAACcQAAAAEHT42omxaLYVQFYKWcCOK+m839Fdggnp8cxjn6Mkn95omfT95QBXmxm1Zz69jCQO4g==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"), new Guid("3969955c-023e-418f-8568-0546f5296145") });

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 11, 23, 51, 22, 990, DateTimeKind.Local).AddTicks(1884));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("3969955c-023e-418f-8568-0546f5296145"));

            migrationBuilder.DeleteData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"), new Guid("3969955c-023e-418f-8568-0546f5296145") });

            migrationBuilder.AlterColumn<int>(
                name: "description",
                table: "AppRole",
                type: "int",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 7, 11, 23, 28, 24, 259, DateTimeKind.Local).AddTicks(6886));
        }
    }
}
