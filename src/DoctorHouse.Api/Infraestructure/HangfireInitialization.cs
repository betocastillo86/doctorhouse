using System;
using DoctorHouse.Business.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorHouse.Api.Infraestructure
{
    public static class HangfireInitialization
    {
        public static void AddHangFire(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration["EnableHangfire"]))
            {
                GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new Hangfire.SqlServer.SqlServerStorageOptions { SchemaName = "Hangfire" });

                var dashboardOptions = new DashboardOptions()
                {
                };

                app.UseHangfireDashboard(pathMatch: "/hangfiredashboard", options: dashboardOptions);

                app.UseHangfireServer();

                StartRecurringJobs();
            }
        }

        public static void RegisterHangFireServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration["EnableHangfire"]))
            {
                services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new Hangfire.SqlServer.SqlServerStorageOptions { SchemaName = "Hangfire" }));
            }
        }

        public static void StartRecurringJobs()
        {
            RecurringJob.AddOrUpdate<SendMailTask>(c => c.SendPendingMails(), "*/2 * * * *");
        }
    }
}