using Microsoft.AspNetCore.Mvc;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for home.
    /// </summary>
    [Route("")]
    [Route("home")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the /home/index page.
        /// </summary>
        /// <returns>Returns the /home/index page.</returns>
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Gets the /home/error page.
        /// </summary>
        /// <returns>Returns the /home/error page.</returns>
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}