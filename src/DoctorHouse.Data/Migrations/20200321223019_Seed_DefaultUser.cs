using System;
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

            migrationBuilder.InsertPlace(
                new Place
                {
                    Description = "the place",
                    CreationDate = DateTime.UtcNow,
                    AvailableFrom = DateTime.UtcNow,
                    AvailableTo = DateTime.UtcNow,
                    GuestAllowed = 2,
                    Phone = "366666",
                    Address = "Cr 10 10 10",
                    LocationId = 1,
                    UserId = 1,
                    Active = true
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}