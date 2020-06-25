using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private IUsersBL _usersBL;
        public HomeController(IUsersBL usersBL)
        {
            _usersBL = usersBL;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}