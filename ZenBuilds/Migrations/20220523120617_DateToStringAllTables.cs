using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenBuilds.Migrations
{
    public partial class DateToStringAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegisterDate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "UserLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<string>(
                name: "LikeDate",
                table: "Likes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<string>(
                name: "FollowDate",
                table: "Followers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<string>(
                name: "Published",
                table: "Builds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserLogs",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LikeDate",
                table: "Likes",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowDate",
                table: "Followers",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Published",
                table: "Builds",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
