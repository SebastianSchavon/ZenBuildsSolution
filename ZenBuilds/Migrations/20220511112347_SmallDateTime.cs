using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenBuilds.Migrations
{
    public partial class SmallDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserLogs",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowDate",
                table: "Followers",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Published",
                table: "Builds",
                type: "SmallDateTime ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowDate",
                table: "Followers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Published",
                table: "Builds",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime ");
        }
    }
}
