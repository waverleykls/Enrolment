using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WaverleyKls.Enrolment.Helpers;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ICookieHelper _helper;

        public HomeController(ICookieHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }

            this._helper = helper;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Route("start-over")]
        [HttpGet]
        public async Task<IActionResult> ClearEnrolmentForm()
        {
            await this._helper.ClearFormIdAsync(this).ConfigureAwait(false);

            return this.RedirectToAction("GetStudentDetailsForm");
        }

        [Route("student-details")]
        [HttpGet]
        public async Task<IActionResult> GetStudentDetailsForm()
        {
            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            var vm = new StudentDetailsViewModel();

            return this.View("StudentDetails", vm);
        }

        [Route("student-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStudentDetails(StudentDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new StudentDetailsViewModel(model);

                return this.View("StudentDetails", vm);
            }

            // TODO: If valid, save student details and move to the next screen.
            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            return this.RedirectToAction("GetGuardianDetailsForm");
        }

        [Route("guardian-details")]
        [HttpGet]
        public IActionResult GetGuardianDetailsForm()
        {
            var vm = new GuardianDetailsViewModel();

            return this.View("GuardianDetails", vm);
        }

        [Route("guardian-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGuardianDetails(GuardianDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new GuardianDetailsViewModel(model);

                return this.View("GuardianDetails", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetStudentDetailsForm"
                                 : "GetEmergencyContactDetailsForm";

            return this.RedirectToAction(actionName);
        }

        [Route("emergency-contact-details")]
        [HttpGet]
        public IActionResult GetEmergencyContactDetailsForm()
        {
            var vm = new EmergencyContactDetailsViewModel();

            return this.View("EmergencyContactDetails", vm);
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

            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetGuardianDetailsForm"
                                 : "GetMedicalDetailsForm";

            if (model.IsSameAsGuardianDetails)
            {
                // TODO: Use parent/guardian details for emergency contact details.

                return this.RedirectToAction(actionName);
            }

            if (!ModelState.IsValid)
            {
                var vm = new EmergencyContactDetailsViewModel(model);

                return this.View("EmergencyContactDetails", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.

            return this.RedirectToAction(actionName);
        }

        [Route("medical-details")]
        [HttpGet]
        public IActionResult GetMedicalDetailsForm()
        {
            var vm = new MedicalDetailsViewModel();

            return this.View("MedicalDetails", vm);
        }

        [Route("medical-details")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetMedicalDetails(MedicalDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new MedicalDetailsViewModel(model);

                return this.View("MedicalDetails", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetEmergencyContactDetailsForm"
                                 : "GetGuardianConsentForm";

            return this.RedirectToAction(actionName);
        }

        [Route("guardian-consents")]
        [HttpGet]
        public IActionResult GetGuardianConsentsForm()
        {
            var vm = new GuardianConsentsViewModel();

            return this.View("GuardianConsents", vm);
        }

        [Route("guardian-consents")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGuardianConsents(GuardianConsentsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new GuardianConsentsViewModel(model);

                return this.View("GuardianConsents", vm);
            }

            // TODO: If valid, save guardian details and move to the next screen.
            var formId = await this._helper.GetFormIdAsync(this).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? "GetMedicalDetailsForm"
                                 : "GetConfirmation";

            return this.RedirectToAction(actionName);
        }
    }
}