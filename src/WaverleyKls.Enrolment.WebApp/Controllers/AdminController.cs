using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for admin.
    /// </summary>
    [Route("admin")]
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// Gets the /admin/index page.
        /// </summary>
        /// <returns>Returns the /admin/index page.</returns>
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Route("sign-out")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}