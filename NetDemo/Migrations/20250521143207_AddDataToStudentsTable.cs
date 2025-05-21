using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStudentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "A", new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "studenta@gmail.com", "Student A" },
                    { 2, "B", new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "studentb@gmail.com", "Student B" }
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
        }
    }
}
