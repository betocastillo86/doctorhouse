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
    }
}