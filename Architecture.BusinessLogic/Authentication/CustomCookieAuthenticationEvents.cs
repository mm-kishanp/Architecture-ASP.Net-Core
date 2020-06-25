using Architecture.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Authentication
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IUser _userRepository;

        public CustomCookieAuthenticationEvents(IUser userRepository)
        {
            // Get the database from registered DI services.
            _userRepository = userRepository;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
             ClaimsPrincipal userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var lastChanged = (from c in userPrincipal.Claims
                               where c.Type == CustomClaimTypes.LastChanged.ToString()
                               select c.Value).FirstOrDefault();

            var userName = (from c in userPrincipal.Claims
                            where c.Type == CustomClaimTypes.Email.ToString()
                            select c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(lastChanged) || !_userRepository.ValidateLastChanged(lastChanged, userName))
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }

}
