using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class UserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users", 
                keyColumn: "Email", 
                keyValue: "test@test.com", 
                column: "UserType", 
                value: 2);
            
            migrationBuilder.UpdateData(
                table: "Users", 
                keyColumn: "Email", 
                keyValue: "test@test.com", 
                column: "UserType", 
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users", 
                keyColumn: "", 
                keyValue: "", 
                column: "UserType", 
                value: null);
        }
    }
}
