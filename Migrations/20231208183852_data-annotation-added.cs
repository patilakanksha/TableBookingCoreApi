using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableBooking.Migrations
{
    public partial class dataannotationadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bookings",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
