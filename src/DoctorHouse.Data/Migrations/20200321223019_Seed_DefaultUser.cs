using System;
using DoctorHouse.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class Seed_DefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertUser(
                new User
                {
                    Name = "Test user",
                    Email = "test@test.com",
                    CreationDate = DateTime.UtcNow,
                    Password = "e173460b741d297359e92a12bf8edcb14439a247", //123456
                    Salt = "F}S2¡7",
                    LocationId = 1
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}