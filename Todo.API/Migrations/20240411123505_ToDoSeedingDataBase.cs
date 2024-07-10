using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo.API.Migrations
{
    /// <inheritdoc />
    public partial class ToDoSeedingDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ToDos",
                columns: new[] { "Id", "DueDate", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 4, 5), true, "Red", "Install Visual Studio" },
                    { 2, new DateOnly(2024, 4, 6), false, "Red", "Configure New Project" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
