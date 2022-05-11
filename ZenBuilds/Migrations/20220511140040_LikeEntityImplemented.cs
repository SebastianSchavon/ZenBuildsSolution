using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenBuilds.Migrations
{
    public partial class LikeEntityImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildUser");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BuildId = table.Column<int>(type: "int", nullable: false),
                    BuildUserId = table.Column<int>(type: "int", nullable: false),
                    BuildId1 = table.Column<int>(type: "int", nullable: false),
                    LikeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.UserId, x.BuildId });
                    table.ForeignKey(
                        name: "FK_Likes_Builds_BuildUserId_BuildId1",
                        columns: x => new { x.BuildUserId, x.BuildId1 },
                        principalTable: "Builds",
                        principalColumns: new[] { "UserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BuildUserId_BuildId1",
                table: "Likes",
                columns: new[] { "BuildUserId", "BuildId1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.CreateTable(
                name: "BuildUser",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "int", nullable: false),
                    LikedBuildsUserId = table.Column<int>(type: "int", nullable: false),
                    LikedBuildsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildUser", x => new { x.LikesId, x.LikedBuildsUserId, x.LikedBuildsId });
                    table.ForeignKey(
                        name: "FK_BuildUser_Builds_LikedBuildsUserId_LikedBuildsId",
                        columns: x => new { x.LikedBuildsUserId, x.LikedBuildsId },
                        principalTable: "Builds",
                        principalColumns: new[] { "UserId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildUser_Users_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildUser_LikedBuildsUserId_LikedBuildsId",
                table: "BuildUser",
                columns: new[] { "LikedBuildsUserId", "LikedBuildsId" });
        }
    }
}
