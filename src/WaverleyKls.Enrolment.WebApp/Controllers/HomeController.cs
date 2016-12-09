using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WaverleyKls.Enrolment.Helpers;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CookieHelper _helper;

        public HomeController()
        {
            this._helper = new CookieHelper(new CookieOptions() { HttpOnly = true, Secure = false });
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Student()
        {
            var formId = this._helper.GetFormId(this);

            var vm = new StudentDetailsViewModel();

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Student(StudentDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new StudentDetailsViewModel(model);

                return this.View(vm);
            }

            // TODO: If valid, save student details and move to the next screen.
            var formId = this._helper.GetFormId(this);

            return this.View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
