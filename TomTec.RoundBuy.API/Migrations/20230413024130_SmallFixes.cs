using Microsoft.EntityFrameworkCore.Migrations;

namespace TomTec.RoundBuy.API.Migrations
{
    public partial class SmallFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueWithouDiscont",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "ValueWithouDiscount",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "AlternativeAddressId",
                table: "Announcements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueWithouDiscount",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "ValueWithouDiscont",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "AlternativeAddressId",
                table: "Announcements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
