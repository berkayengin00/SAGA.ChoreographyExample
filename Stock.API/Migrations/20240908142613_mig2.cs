using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "ProductId", "Quntity" },
                values: new object[,]
                {
                    { new Guid("44d4a41b-69be-49a0-a5b2-94c62f3d3a62"), new Guid("0bd39273-fcc6-4167-b1e9-88fccf9dfa2e"), 5 },
                    { new Guid("a5b3f877-8816-4f8e-ade4-ea1641b5c4a4"), new Guid("25bba58e-c7c5-4dca-ab2f-6ca0fbc98f09"), 14 },
                    { new Guid("de81ba9e-b2c7-4f01-8ab4-666f40566e7e"), new Guid("7e8e9773-2670-4e06-9115-f1e4aa8acf22"), 13 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: new Guid("44d4a41b-69be-49a0-a5b2-94c62f3d3a62"));

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: new Guid("a5b3f877-8816-4f8e-ade4-ea1641b5c4a4"));

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: new Guid("de81ba9e-b2c7-4f01-8ab4-666f40566e7e"));
        }
    }
}
