using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class SeedLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertLocation(new Location { Name = "Colombia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}