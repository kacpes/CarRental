using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.API.Migrations
{
    public partial class fixedseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarCategories",
                keyColumn: "CarCategoryId",
                keyValue: 1,
                column: "CarType",
                value: "Compact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarCategories",
                keyColumn: "CarCategoryId",
                keyValue: 1,
                column: "CarType",
                value: "Regular");
        }
    }
}
