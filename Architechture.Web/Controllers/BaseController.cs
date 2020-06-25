using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture.Web.Configuration;
using Architecture.BusinessLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Architecture.Web.Controllers
{
    [ServiceFilter(typeof(LogConstantFilter))]
    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]
    public class BaseController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public readonly SiteConfiguration _siteConfiguration;

        public BaseController(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, SiteConfiguration siteConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _siteConfiguration = siteConfiguration;
        }
    }
}