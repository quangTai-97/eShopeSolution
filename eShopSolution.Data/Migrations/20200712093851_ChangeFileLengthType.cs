using Microsoft.EntityFrameworkCore.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("3969955c-023e-418f-8568-0546f5296145"),
                column: "ConcurrencyStamp",
                value: "b42ba013-a701-42ee-ba48-5ebf880c7a3a");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3042713d-0a6a-4110-a6fa-f03d57286816", "AQAAAAEAACcQAAAAENeGksoRTHJiC2W9ReDtEkkDqRza71n/eu9/oUJVAcJrrlF36n6HAcVX+SFls+hz/g==" });

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
                value: new DateTime(2020, 7, 12, 16, 38, 49, 838, DateTimeKind.Local).AddTicks(3779));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("3969955c-023e-418f-8568-0546f5296145"),
                column: "ConcurrencyStamp",
                value: "22095ca9-fd0c-4962-ad10-250159533218");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0f7c6468-7178-4e2e-8e9c-e822abca168d", "AQAAAAEAACcQAAAAEIfpo43mvNSPKgfwqKfgvp3EnQLKI8WcqVMEcaD4QMyL7QHiv8/VdoHJP0kqpJLkTg==" });

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
                value: new DateTime(2020, 7, 12, 14, 45, 16, 824, DateTimeKind.Local).AddTicks(6636));
        }
    }
}
