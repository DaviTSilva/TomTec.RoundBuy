using Microsoft.EntityFrameworkCore.Migrations;

namespace TomTec.RoundBuy.API.Migrations
{
    public partial class FieldsFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPorcentage",
                table: "Announcements",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPorcentage",
                table: "Announcements");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
