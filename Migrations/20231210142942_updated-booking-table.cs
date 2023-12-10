using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Migrations
{
    public partial class updatedbookingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingTime",
                table: "Bookings",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Bookings",
                newName: "BookingTime");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
