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

        public static void InsertPlace(this MigrationBuilder migrationBuilder, Place entity)
        {
            migrationBuilder.InsertData(
               "Places",
               new string[]
               {
                    "Latitude",
                    "Logitude",
                    "Address",
                    "Phone",
                    "Description",
                    "GuestAllowed",
                    "Bathroom",
                    "Food",
                    "Kitchen",
                    "Parking",
                    "UserId",
                    "AvailableFrom",
                    "AvailableTo",
                    "Active",
                    "Deleted",
                    "CreationDate",
                    "Internet",
                    "EntireHouse",
               },
               new object[]
               {
                    entity.Latitude,
                    entity.Logitude,
                    entity.Address,
                    entity.Phone,
                    entity.Description,
                    entity.GuestAllowed,
                    entity.Bathroom,
                    entity.Food,
                    entity.Kitchen,
                    entity.Parking,
                    entity.UserId,
                    entity.AvailableFrom,
                    entity.AvailableTo,
                    entity.Active,
                    entity.Deleted,
                    entity.CreationDate,
                    entity.Internet,
                    entity.EntireHouse
               });
        }
    }
}