using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Solution.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a27baa8-2c8e-479c-b8ab-7c4f8f6d38ef", "f0022fd2-2a62-4d66-a8af-bf3f7d5efc02", "Administrator", "ADMINISTRATOR" },
                    { "f91f2a98-0f27-4988-a80c-548c90dd4f83", "df050077-43b2-4df3-b916-8baaaf1c58a0", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a27baa8-2c8e-479c-b8ab-7c4f8f6d38ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f91f2a98-0f27-4988-a80c-548c90dd4f83");
        }
    }
}
