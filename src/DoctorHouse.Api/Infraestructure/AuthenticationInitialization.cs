using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorHouse.Api.Infraestructure
{
    public static class AuthenticationInitialization
    {
        public static void RegisterAuthenticationServices(this IServiceCollection services)
        {
            services.AddAuthentication(Microsoft.AspNetCore.Server.HttpSys.HttpSysDefaults.AuthenticationScheme)
                .AddOAuthValidation(Microsoft.AspNetCore.Server.HttpSys.HttpSysDefaults.AuthenticationScheme)
                .AddOpenIdConnectServer(c =>
                {
                    c.Provider = new DoctorHouseAuthorizationProvider();

                    c.AccessTokenLifetime = new System.TimeSpan(150, 0, 0);

                    // Enable the authorization and token endpoints.
                    c.TokenEndpointPath = "/api/v1/auth";

                    c.AllowInsecureHttp = true;
                });
        }

        public static void ConfigureAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}