using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class AlterColumn_Latitude_Longitude_Table_Place : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestAllowed",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Logitude",
                table: "Places");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableTo",
                table: "Places",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Places",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<byte>(
                name: "GuestsAllowed",
                table: "Places",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestsAllowed",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Places");

            migrationBuilder.AlterColumn<long>(
                name: "Latitude",
                table: "Places",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableTo",
                table: "Places",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AvailableFrom",
                table: "Places",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "GuestAllowed",
                table: "Places",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<long>(
                name: "Logitude",
                table: "Places",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
