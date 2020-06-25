using Architecture.Web.Configuration;
using Architecture.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Architecture.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider ,SiteConfiguration siteConfiguration) : base(hostingEnvironment, modelMetadataProvider, siteConfiguration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
