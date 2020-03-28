using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class RemoveColumn_OwnerId_Table_Request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_UserOwnerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserOwnerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserOwnerId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserOwnerId",
                table: "Requests",
                column: "UserOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_UserOwnerId",
                table: "Requests",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
