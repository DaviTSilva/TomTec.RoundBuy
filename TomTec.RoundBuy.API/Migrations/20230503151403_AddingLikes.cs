using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TomTec.RoundBuy.API.Migrations
{
    public partial class AddingLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLikesOnAnnouncements",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikesOnAnnouncements", x => new { x.UserId, x.AnnouncementId });
                    table.ForeignKey(
                        name: "FK_UserLikesOnAnnouncements_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikesOnAnnouncements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLikesOnComments",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikesOnComments", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_UserLikesOnComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikesOnComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLikesOnRatings",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RatingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikesOnRatings", x => new { x.UserId, x.RatingId });
                    table.ForeignKey(
                        name: "FK_UserLikesOnRatings_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikesOnRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 975, DateTimeKind.Utc).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 975, DateTimeKind.Utc).AddTicks(7824));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 977, DateTimeKind.Utc).AddTicks(8577));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 977, DateTimeKind.Utc).AddTicks(8720));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 977, DateTimeKind.Utc).AddTicks(8761));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 976, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 15, 14, 2, 976, DateTimeKind.Utc).AddTicks(7879));

            migrationBuilder.CreateIndex(
                name: "IX_UserLikesOnAnnouncements_AnnouncementId",
                table: "UserLikesOnAnnouncements",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikesOnComments_CommentId",
                table: "UserLikesOnComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikesOnRatings_RatingId",
                table: "UserLikesOnRatings",
                column: "RatingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLikesOnAnnouncements");

            migrationBuilder.DropTable(
                name: "UserLikesOnComments");

            migrationBuilder.DropTable(
                name: "UserLikesOnRatings");

            migrationBuilder.UpdateData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 244, DateTimeKind.Utc).AddTicks(6055));

            migrationBuilder.UpdateData(
                table: "OfficialIdentificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 244, DateTimeKind.Utc).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 247, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 246, DateTimeKind.Utc).AddTicks(739));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2023, 5, 3, 7, 40, 13, 246, DateTimeKind.Utc).AddTicks(962));
        }
    }
}
