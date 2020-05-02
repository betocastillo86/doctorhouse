using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoctorHouse.Data.Migrations
{
    public partial class AddTable_Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertPlace(
                new Place
                {
                    Description = "the place",
                    CreationDate = DateTime.UtcNow,
                    AvailableFrom = DateTime.UtcNow,
                    AvailableTo = DateTime.UtcNow,
                    GuestsAllowed = 2,
                    Phone = "366666",
                    Address = "Cr 10 10 10",
                    LocationId = 1,
                    UserId = 1,
                    Active = true
                });

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IOsDeviceId",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.CreateTable(
                name: "EmailNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    To = table.Column<string>(maxLength: 200, nullable: false),
                    ToName = table.Column<string>(maxLength: 200, nullable: true),
                    CC = table.Column<string>(maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "varchar(300)", nullable: false),
                    Body = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: true),
                    SentDate = table.Column<DateTime>(nullable: true),
                    SentTries = table.Column<short>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EmailHtml = table.Column<string>(nullable: true),
                    EmailSubject = table.Column<string>(maxLength: 500, nullable: true),
                    IsEmail = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    SystemText = table.Column<string>(maxLength: 2000, nullable: true),
                    Tags = table.Column<string>(maxLength: 3000, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    IsMobile = table.Column<bool>(nullable: false),
                    MobileText = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_IsSent",
                table: "EmailNotifications",
                column: "IsSent");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_SentTries_SentDate",
                table: "EmailNotifications",
                columns: new[] { "SentTries", "SentDate" });

            migrationBuilder.InsertNotification(new Notification
            {
                Id = Convert.ToInt32(NotificationType.NewRequest),
                Active = true,
                EmailHtml = "Has recibido una nueva solicitud de hospedaje por parte de %%UserRequester.Name%% - %%UserRequester.PhoneNumber%%. <br><br> Puedes ver la solcitud acá %%Url%%",
                EmailSubject = "Has recibido una nueva solicitud de hospedaje",
                IsEmail = true,
                Name = "Nueva solicitud de hospedaje",
                Tags = string.Empty
            });

            migrationBuilder.InsertNotification(new Notification
            {
                Id = Convert.ToInt32(NotificationType.RequestAnswered),
                Active = true,
                EmailHtml = "Tu solicitud ha sido validada por %%UserHost.Name%% y ha sido %%Request.Status%%. <br> <br> Los comentarios del propietario son: %%Request.DescriptionAnswer%%",
                EmailSubject = "Tu solicitud  de hospedaje ha sido respondida",
                IsEmail = true,
                Name = "Solicitud respondida",
                Tags = string.Empty
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailNotifications");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IOsDeviceId",
                table: "Users");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Places",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }
    }
}