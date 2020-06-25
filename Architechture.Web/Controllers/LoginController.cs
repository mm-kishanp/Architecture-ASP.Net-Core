using Architecture.Web.Configuration;
using Architecture.BusinessLogic;
using Architecture.Entities;
using Architecture.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Architecture.Web.Controllers
{
    public class LoginController : BaseController
    {
        private IUsersBL _usersBL;

        public LoginController(IUsersBL usersBL, IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider,SiteConfiguration siteConfiguration) : base(hostingEnvironment, modelMetadataProvider, siteConfiguration)
        {
            _usersBL = usersBL;
        }

        #region Login Logout
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await new CustomAuthenticationService().SignOutAsync(HttpContext);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginViewModelResponse validatUser = _usersBL.Validateuser(model.UserName, model.Password);
                if (validatUser != null)
                {
                    await new CustomAuthenticationService().SignInUserAsync(validatUser, HttpContext);
                }
                else
                {
                    ModelState.AddModelError("UserName", "Credentials are not valid, Please try again.");
                    return View("Index", model);
                }
            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
        #endregion

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(LoginViewModel model)
        {
            ModelState["Password"].Errors.Clear();
            if (ModelState.IsValid)
            {
                if (_usersBL.CheckEmail(model.UserName))
                {
                    _usersBL.GeneratePassword(model.UserName,CommonHelper.GenerateRandomDigitCode(6), _siteConfiguration.customConfiguration.CommonSettings.PasswordEncryptionSecurityKey);
                }
                else
                {
                    ModelState.AddModelError("UserName", "User not exist in the system.");
                    return View("Index", model);
                }
            }
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
    }
}
