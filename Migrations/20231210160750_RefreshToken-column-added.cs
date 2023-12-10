using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Migrations
{
    public partial class RefreshTokencolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshAccessToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshAccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Users");
        }
    }
}
