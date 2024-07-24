using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class defaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Bangalore", new DateTime(1982, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test@1", "Madhu" },
                    { 2, "Bangalore", new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test@2", "Dharani" },
                    { 3, "Bangalore", new DateTime(1982, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test@3", "MadhuDharani" },
                    { 4, "Bangalore", new DateTime(2001, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test@4", "DharaniMadhu" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Students");
        }
    }
}
