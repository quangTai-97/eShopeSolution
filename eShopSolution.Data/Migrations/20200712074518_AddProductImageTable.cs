using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeafault = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("3969955c-023e-418f-8568-0546f5296145"),
                column: "ConcurrencyStamp",
                value: "e428d7c0-69b4-4709-af0f-952d2964ea0a");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("e2c7e83d-f2ae-45f6-964b-c42e3c826a1b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69f193b5-27ae-45d5-b909-ec1b575df150", "AQAAAAEAACcQAAAAEHT42omxaLYVQFYKWcCOK+m839Fdggnp8cxjn6Mkn95omfT95QBXmxm1Zz69jCQO4g==" });

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
    }
}
