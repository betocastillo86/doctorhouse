using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public static class MigrationBuilderExtensions
    {
        public static void InsertLocation(this MigrationBuilder migrationBuilder, Location entity)
        {
            migrationBuilder.InsertData(
               "Locations",
               new string[]
               {
                    "Name",
                    "ParentLocationId",
                    "Deleted"
               },
               new object[]
               {
                    entity.Name,
                    entity.ParentLocationId,
                    false
               });
        }

        public static void InsertUser(this MigrationBuilder migrationBuilder, User entity)
        {
            migrationBuilder.InsertData(
               "Users",
               new string[]
               {
                    "Name",
                    "Email",
                    "LocationId",
                    "CreationDate",
                    "Password",
                    "PasswordRecoveryToken",
                    "Salt",
                    "Deleted",
                    "JobAddress",
                    "JobPlace",
                    "PhoneNumber"
               },
               new object[]
               {
                    entity.Name,
                    entity.Email,
                    entity.LocationId,
                    entity.CreationDate,
                    entity.Password,
                    entity.PasswordRecoveryToken,
                    entity.Salt,
                    entity.Deleted,
                    entity.JobAddress,
                    entity.JobPlace,
                    entity.PhoneNumber
               });
        }
    }
}