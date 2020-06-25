using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;

namespace Architecture.Utility
{
    public class WebHelper
    { /// <summary>
      /// Gets whether the request is made with AJAX
      /// </summary>
      /// <param name="request">HTTP request</param>
      /// <returns>Result</returns>
        public virtual bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers == null)
            {
                return false;
            }

            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// Get IP address from HTTP context
        /// </summary>
        /// <returns>String of IP address</returns>
        public virtual string GetCurrentIpAddress(HttpContext httpContext)
        {
            if (!IsRequestAvailable(httpContext))
                return string.Empty;

            var result = string.Empty;
            try
            {
                //if this header not exists try get connection remote IP address
                if (string.IsNullOrEmpty(result) && httpContext.Connection.RemoteIpAddress != null)
                    result = httpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }

            //some of the validation
            if (result != null && result.Equals(IPAddress.IPv6Loopback.ToString(), StringComparison.InvariantCultureIgnoreCase))
                result = IPAddress.Loopback.ToString();

            //"TryParse" doesn't support IPv4 with port number
            if (IPAddress.TryParse(result ?? string.Empty, out var ip))
                //IP address is valid
                result = ip.ToString();
            else if (!string.IsNullOrEmpty(result))
                //remove port
                result = result.Split(':').FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Check whether current HTTP request is available
        /// </summary>
        /// <returns>True if available; otherwise false</returns>
        protected virtual bool IsRequestAvailable(HttpContext httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}