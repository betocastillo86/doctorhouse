using System;
using System.Collections.Generic;
using Beto.Core.Data;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DoctorHouse.Api.Tests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<DoctorHouseContext>(options =>
                {
                    options.UseInMemoryDatabase("DoctorHouse");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IDbContext>(provider => provider.GetService<DoctorHouseContext>());

                var scopeServiceProvider = services.BuildServiceProvider();
                using var scope = scopeServiceProvider.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<DoctorHouseContext>();
                var logger = scopedServices.GetRequiredService<ILogger<ApiWebApplicationFactory>>();

                context.Database.EnsureCreated();
                this.SeedTestData(context);
            }).UseEnvironment("Test");
        }

        private void SeedTestData(DoctorHouseContext context)
        {
            this.SeedUsers(context);
            this.SeedLocations(context);
            this.SeedPlaces(context);
        }

        private void SeedUsers(DoctorHouseContext context)
        {
            var users = new List<User>()
            {
                new User
                {
                    Name = "Test user",
                    Email = "test@test.com",
                    CreationDate = DateTime.UtcNow,
                    Password = "e173460b741d297359e92a12bf8edcb14439a247", //123456
                    Salt = "F}S2¡7",
                    LocationId = 1
                }
            };

            context.AddRange(users);

            context.SaveChanges();
        }

        private void SeedLocations(DoctorHouseContext context)
        {
            var locations = new List<Location>()
            {
                new Location { Name = "Colombia" },
                new Location { Name = "Argentina" }
            };

            context.AddRange(locations);

            context.SaveChanges();
        }

        private void SeedPlaces(DoctorHouseContext context)
        {
            var places = new List<Place>()
            {
                new Place
                {
                    Active = true,
                    Address = "address",
                    CreationDate = DateTime.UtcNow,
                    Description = "description",
                    GuestsAllowed = 1,
                    Latitude = 1,
                    Longitude = 1,
                    UserId = 1,
                    LocationId = 1,
                    Phone = "123456"
                }
            };

            context.AddRange(places);

            context.SaveChanges();
        }
    }
}