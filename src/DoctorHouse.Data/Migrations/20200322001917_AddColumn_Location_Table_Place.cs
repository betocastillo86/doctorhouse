using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class AddColumn_Location_Table_Place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Places",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Places_LocationId",
                table: "Places",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Locations_LocationId",
                table: "Places",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Locations_LocationId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_LocationId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Places");
        }
    }
}
