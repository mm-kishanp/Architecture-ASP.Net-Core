using Architecture.ILogger.Serilog;
using Architecture.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Linq;

namespace Architecture.BusinessLogic
{
    public class LogConstantFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Logger.UserActionLoggin(JsonSerializeDeserializer.JsonString(new SeriLogEntryEntity()
            {
                QueryString = context.HttpContext.Request.QueryString.Value,
                RequetForm = context.HttpContext.Request.HasFormContentType ? string.Join(",", context.HttpContext.Request.Form.Select(x => "\""+x.Key + "\"" + " : " + "\"" + x.Value+"\"" )) :"",
                TimeStamp = DateTime.UtcNow,
                ActionDescriptor = context.ActionDescriptor.DisplayName,
                IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                //Message = context.Exception.Message,
                RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                RequestPath = context.HttpContext.Request.Path,
                User = context.HttpContext.User.HasClaim(x => x.Type == CustomClaimTypes.Email.ToString()) ? context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Email.ToString()).Value : null
            }), Serilog.Events.LogEventLevel.Information);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
