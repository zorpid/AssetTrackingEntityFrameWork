using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssetTrackingEntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class AddingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfLifeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    AssetType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Currency", "ExchangeRate", "Name" },
                values: new object[,]
                {
                    { 1, "USD", 1.0m, "New York" },
                    { 2, "GBP", 0.75m, "London" },
                    { 3, "JPY", 110.0m, "Tokyo" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetType", "EndOfLifeDate", "ModelName", "Name", "OfficeId", "PurchaseDate", "PurchasePrice" },
                values: new object[,]
                {
                    { 1, "Laptop", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4521), "Air", "MacBook", 1, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4480), 2000m },
                    { 2, "Laptop", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4525), "S33", "Lenovo", 2, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4524), 2000m },
                    { 3, "Laptop", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4530), "Rog 24", "Asus", 3, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4528), 2000m },
                    { 4, "Mobile", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4554), "15", "iPhone", 1, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4552), 1200m },
                    { 5, "Mobile", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4558), "Galaxy 12", "Samsung", 2, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4556), 1200m },
                    { 6, "Mobile", new DateTime(2027, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4562), "3310", "Nokia", 3, new DateTime(2024, 2, 7, 14, 45, 40, 499, DateTimeKind.Local).AddTicks(4560), 1200m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OfficeId",
                table: "Assets",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
