using Architecture.Utility;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Architecture.Web.Configuration
{
    public class SiteConfiguration
    {
        public const string xmlDocumentPath = "SiteConfiguration";
        public const string xmlDocumentPathFileName = "SiteConfiguration.xml";
        private readonly IHostingEnvironment _host;
        public SiteConfiguration(IHostingEnvironment host)
        {
            _host = host;
            customConfiguration = XMLSerializeDeSerialize.DeSerializeObject<CustomConfiguration>(Path.Combine(_host.ContentRootPath, xmlDocumentPath, xmlDocumentPathFileName));
        }

        public CustomConfiguration customConfiguration;
    }
    #region XML conversion models
    public class CustomConfiguration
    {
        public ErrorMessages ErrorMessages { get; set; }
        public CommonSettings CommonSettings { get; set; }
    }
    public class ErrorMessages
    {
        public Error Please_fill_mandatory_fields { get; set; }
        public Error User_is_not_exist { get; set; }
        public Error Internal_error { get; set; }
    }
    public class CommonSettings
    {
        public string PasswordEncryptionSecurityKey{ get; set; }
    }
    public class Error
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
    #endregion
}
