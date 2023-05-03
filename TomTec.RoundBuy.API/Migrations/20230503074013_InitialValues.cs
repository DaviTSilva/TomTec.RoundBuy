using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TomTec.RoundBuy.API.Migrations
{
    public partial class InitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OfficialIdentificationTypes",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 3, 7, 40, 13, 244, DateTimeKind.Utc).AddTicks(6055), "CPF" },
                    { 2, new DateTime(2023, 5, 3, 7, 40, 13, 244, DateTimeKind.Utc).AddTicks(6748), "CPNJ" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4164), "CreditCard" },
                    { 2, new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4371), "Boleto" },
                    { 3, new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4429), "Pix" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "CreationDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 3, 7, 40, 13, 246, DateTimeKind.Utc).AddTicks(739), "Prod" },
                    { 2, new DateTime(2023, 5, 3, 7, 40, 13, 246, DateTimeKind.Utc).AddTicks(962), "Test" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficialIdentificationTypes_Name",
                table: "OfficialIdentificationTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OfficialIdentificationTypes_Name",
                table: "OfficialIdentificationTypes");

            migrationBuilder.DeleteData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
