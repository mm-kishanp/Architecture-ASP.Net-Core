using Architecture.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Architecture.BusinessLogic.Authentication
{
    public class PermissionFilter : Attribute, IActionFilter
    {
        private readonly string _permission;
        private readonly CustomClaimTypes _customClaimTypes;

        public PermissionFilter(string permission, CustomClaimTypes customClaimTypes)
        {
            _permission = permission;
            _customClaimTypes = customClaimTypes;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ClaimsPrincipal userPrincipal = context.HttpContext.User;
            var UserRole = (from c in userPrincipal.Claims where c.Type == _customClaimTypes.ToString() select c.Value).FirstOrDefault();
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
            else if (!UserRole.ThisPermissionIsAllowed(_permission))
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