using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Noggin.NetCoreAuth.Model;
using Noggin.NetCoreAuth.Providers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UrlShortener.Start
{
    public class SocialLoginHandler : ILoginHandler
    {
        private readonly IConfiguration _config;

        public SocialLoginHandler(IConfiguration config)
        {
            _config = config;
        }

        public Task<IActionResult> FailedLoginFrom(string provider, AuthenticationFailInformation failInfo, HttpContext httpContext)
        {
            return Task.FromResult(new RedirectToActionResult("Index", "Home", null) as IActionResult);
        }

        public async Task<IActionResult> SuccessfulLoginFrom(string provider, UserInformation user, HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                var adminUserName = _config.GetValue<string>("AdminUser");
                if (user.UserName.Equals(adminUserName, StringComparison.OrdinalIgnoreCase))
                {
                    await SignUserIn(adminUserName, httpContext);
                    
                }
                else
                {
                    throw new NotImplementedException("Bugger off");
                }
            }

            return new RedirectToActionResult("Index", "Home", null);
        }

        /// <summary>
		/// Create a principal to login with containing claims with info about the user
		/// </summary>
		private static ClaimsPrincipal CreatePrincipal(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", userName),
                new Claim("UserName", userName)
            };
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            return principal;
        }

        private static async Task SignUserIn(string userName, HttpContext httpContext)
        {
            // Using Cookie Authentication without ASP.NET Core Identity
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x

            // Todo:
            // * Is it okay to create an empty principal in this simple case
            // * Is this simple policy okay, create as constant in class perhaps
            var principal = CreatePrincipal(userName);
            //var policy = new OperationAuthorizationRequirement { Name = "All" };

            // https://stackoverflow.com/questions/46057109/why-doesnt-my-cookie-authentication-work-in-asp-net-core
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
