using System;
using System.Reflection;
using Beto.Core.Caching;
using Beto.Core.Data;
using Beto.Core.Data.Configuration;
using Beto.Core.Data.Notifications;
using Beto.Core.EventPublisher;
using Beto.Core.Exceptions;
using Beto.Core.Helpers;
using Beto.Core.Registers;
using DoctorHouse.Business.Exceptions;
using DoctorHouse.Business.Security;
using DoctorHouse.Business.Services;
using DoctorHouse.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

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

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPlaceService, PlaceService>();

            services.AddScoped<IGuestService, GuestService>();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<ILocationService, LocationService>();

            services.AddMemoryCache();

            RegisterCoreServices(services, configuration);
        }

        private static void RegisterCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWorkContext, WorkContext>();

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IMessageExceptionFinder, MessageExceptionFinder>();

            services.AddScoped<IDbContext, DoctorHouseContext>();

            services.AddScoped<IHttpContextHelper, HttpContextHelper>();

            services.AddScoped<IServiceFactory, DefaultServiceFactory>();

            services.AddScoped<ICacheManager, MemoryCacheManager>();

            services.AddScoped<IPublisher, Publisher>();

            services.AddScoped<ICoreNotificationService, CoreNotificationService>();

            services.AddScoped<ICoreSettingService, CoreSettingService>();

            services.AddScoped<IWebCacheService, WebCacheService>();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IConfiguration>(configuration);

            foreach (var implementationType in ReflectionHelper.GetTypesOnProject(typeof(ISubscriber<>), "doctorhouse"))
            {
                var servicesTypeFound = implementationType.GetTypeInfo().FindInterfaces(
                    (type, criteria) =>
                    {
                        return type.GetTypeInfo().IsGenericType && ((Type)criteria).GetTypeInfo().IsAssignableFrom(type.GetGenericTypeDefinition());
                    },
                    typeof(ISubscriber<>));

                foreach (var serviceFoundType in servicesTypeFound)
                {
                    services.AddScoped(serviceFoundType, implementationType);
                }
            }
        }
    }
}