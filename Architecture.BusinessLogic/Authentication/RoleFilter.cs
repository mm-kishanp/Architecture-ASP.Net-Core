using Architecture.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Architecture.BusinessLogic.Authentication
{
    public class RoleFilter : Attribute, IActionFilter
    {
        private readonly string _RoleName;

        public RoleFilter(string RoleName)
        {
            _RoleName = RoleName;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ClaimsPrincipal userPrincipal = context.HttpContext.User;
            var UserRole = (from c in userPrincipal.Claims where c.Type == CustomClaimTypes.RoleName.ToString() select c.Value).FirstOrDefault();
            if (UserRole == null)
            {
                if (!context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new RedirectResult("~/Error/AuthorizationError");
                }
                else
                {
                    context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
                }
            }
            else if (!UserRole.ThisPermissionIsAllowed(_RoleName))
            {
                context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}