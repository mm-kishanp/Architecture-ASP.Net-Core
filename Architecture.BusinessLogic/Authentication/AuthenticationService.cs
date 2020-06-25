using Architecture.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic
{
    public class CustomAuthenticationService
    {
        public async Task SignInUserAsync(LoginViewModelResponse model, HttpContext httpContext)
        {
            // CHECK BEFORE THIS USER IS VALID OR NOT THEN BELOW CODE SHOULD EXECUTE.
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Email.ToString(), model.UserName),
                new Claim(CustomClaimTypes.LastChanged.ToString(),  DateTime.Now.ToString()),
                new Claim(CustomClaimTypes.RoleId.ToString(),  model.RoleId.Aggregate( (result, item) => result.ToString() +","+ item.ToString())),
                new Claim(CustomClaimTypes.RoleName.ToString(),  model.RoleName.Aggregate( (result, item) => result.ToString() +","+ item.ToString())),
                new Claim(CustomClaimTypes.Password.ToString(),  model.Password)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await httpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity),
                       authProperties);
        }
        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
