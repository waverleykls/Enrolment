using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WaverleyKls.Enrolment.ViewModels;
using WaverleyKls.Enrolment.WebApp.Contexts;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IEnrolmentContext _context;

        public HomeController(IEnrolmentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Route("start-over")]
        [HttpGet]
        public async Task<IActionResult> ClearEnrolmentForm()
        {
            await this._context.CookieHelper.ClearFormIdAsync(this).ConfigureAwait(false);

            return this.RedirectToAction("GetStudentDetailsForm");
        }

        [Route("student-details")]
        [HttpGet]
        public async Task<IActionResult> GetStudentDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.StudentDetailsService.GetStudentDetailsAsync(formId).ConfigureAwait(false) ??
                        new StudentDetailsViewModel();

            return this.View("StudentDetails", model.Clone());
        }

        [Route("student-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStudentDetails(StudentDetailsViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("StudentDetails", vm);
            }

            // TODO: If valid, save student details and move to the next screen.
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.StudentDetailsService.SaveStudentDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            return this.RedirectToAction("GetGuardianDetailsForm");
        }

        [Route("guardian-details")]
        [HttpGet]
        public async Task<IActionResult> GetGuardianDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.GuardianDetailsService.GetGuardianDetailsAsync(formId).ConfigureAwait(false) ??
                        new GuardianDetailsViewModel();

            return this.View("GuardianDetails", model.Clone());
        }

        [Route("guardian-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGuardianDetails(GuardianDetailsViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("GuardianDetails", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.GuardianDetailsService.SaveGuardianDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetStudentDetailsForm"
                                 : "GetEmergencyContactDetailsForm";

            return this.RedirectToAction(actionName);
        }

        [Route("emergency-contact-details")]
        [HttpGet]
        public async Task<IActionResult> GetEmergencyContactDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.EmergencyContactDetailsService.GetEmergencyContactDetailsAsync(formId).ConfigureAwait(false) ??
                        new EmergencyContactDetailsViewModel();

            model = await this._context.EmergencyContactDetailsService.MergeGuardianDetailsAsync(formId, model).ConfigureAwait(false);

            return this.View("EmergencyContactDetails", model.Clone());
        }

        [Route("emergency-contact-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetEmergencyContactDetails(EmergencyContactDetailsViewModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("EmergencyContactDetails", vm);
            }

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.EmergencyContactDetailsService.SaveEmergencyContactDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetGuardianDetailsForm"
                                 : "GetMedicalDetailsForm";

            return this.RedirectToAction(actionName);
        }

        [Route("medical-details")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.MedicalDetailsService.GetMedicalDetailsAsync(formId).ConfigureAwait(false) ??
                        new MedicalDetailsViewModel();

            return this.View("MedicalDetails", model.Clone());
        }

        [Route("medical-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetMedicalDetails(MedicalDetailsViewModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("MedicalDetails", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.MedicalDetailsService.SaveMedicalDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetEmergencyContactDetailsForm"
                                 : "GetGuardianConsentsForm";

            return this.RedirectToAction(actionName);
        }

        [Route("guardian-consents")]
        [HttpGet]
        public async Task<IActionResult> GetGuardianConsentsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.GuardianConsentsService.GetGuardianConsentsAsync(formId).ConfigureAwait(false) ??
                        new GuardianConsentsViewModel();

            return this.View("GuardianConsents", model.Clone());
        }

        [Route("guardian-consents")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGuardianConsents(GuardianConsentsViewModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            if (!model.AgreeToc || !model.AgreePhoto || !model.AgreeSms || !model.AgreeKakaoTalk)
            {
                ModelState.AddModelError("AgreeToc", "ToC must be ticked");
            }

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("GuardianConsents", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.GuardianConsentsService.SaveGuardianConsentsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetMedicalDetailsForm"
                                 : "GetConfirmation";

            return this.RedirectToAction(actionName);
        }
    }
}