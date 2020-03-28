using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class AddColumn_JobData_Table_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordRecoveryToken",
                table: "Users",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "JobAddress",
                table: "Users",
                maxLength: 100,
                nullable: true);
            
            migrationBuilder.AddColumn<string>(
                name: "JobPlace",
                table: "Users",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobAddress",
                table: "Users");
            
            migrationBuilder.DropColumn(
                name: "JobPlace",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordRecoveryToken",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
