using System;
using DoctorHouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorHouse.Api.Infraestructure
{
    public static class ServiceRegister
    {
        public static void RegisterHouseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DoctorHouseContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(60);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(15),
                            errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly("DoctorHouse.Data");
                    });
            });
        }
    }
}