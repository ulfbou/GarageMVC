using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageMVC.Migrations
{
    /// <inheritdoc />
    public partial class fixedplace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotNumber",
                table: "ParkedVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingSpotNumber",
                table: "ParkedVehicles");
        }
    }
}
