using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using DoctorHouse.Business.Services;
using Microsoft.AspNetCore.Authentication;

namespace DoctorHouse.Api.Infraestructure
{
    public class DoctorHouseAuthorizationProvider : OpenIdConnectServerProvider
    {
        public override async Task HandleTokenRequest(HandleTokenRequestContext context)
        {
            if (context.Request.IsPasswordGrantType())
            {
                var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

                var user = await userService.ValidateAuthentication(context.Request.Username, context.Request.Password);

                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Scheme.Name);

                    identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.Id.ToString());
                    identity.AddClaim(OpenIdConnectConstants.Claims.Name, user.Name);

                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties(),
                        context.Scheme.Name);

                    ticket.SetScopes(
                        OpenIdConnectConstants.Scopes.OpenId,
                        OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Profile);

                    ticket.SetResources("https://doctorhouse.azurewebsites.net");

                    context.Validate(ticket);
                }
                else
                {
                    context.Reject(
                        error: OpenIdConnectConstants.Errors.AccessDenied,
                        description: "Los datos ingresados son invalidos");
                }
            }
        }

        public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            // Reject the token requests that don't use grant_type=password or grant_type=refresh_token.
            if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                           error: OpenIdConnectConstants.Errors.InvalidClient,
                           description: "Tipo de autenticación inválida");

                return Task.FromResult(0);
            }

            context.Skip();

            return Task.FromResult(0);
        }
    }
}