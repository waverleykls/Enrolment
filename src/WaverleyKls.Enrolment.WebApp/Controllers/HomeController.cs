using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WaverleyKls.Enrolment.ViewModels;
using WaverleyKls.Enrolment.WebApp.Contexts;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for enrolment form.
    /// </summary>
    [Route("")]
    public class HomeController : Controller
    {
        private const string StartOverGet = "ClearEnrolmentForm";
        private const string StudentDetailsGet = "GetStudentDetailsForm";
        private const string StudentDetailsPost = "SetStudentDetails";
        private const string GuardianDetailsGet = "GetGuardianDetailsForm";
        private const string GuardianDetailsPost = "SetGuardianDetails";
        private const string EmergencyContactDetailsGet = "GetEmergencyContactDetailsForm";
        private const string EmergencyContactDetailsPost = "SetEmergencyContactDetails";
        private const string MedicalDetailsGet = "GetMedicalDetailsForm";
        private const string MedicalDetailsPost = "SetMedicalDetails";
        private const string GuardianConsentsGet = "GetGuardianConsentsForm";
        private const string GuardianConsentsPost = "SetGuardianConsents";
        private const string ConfirmationGet = "GetConfirmation";
        private const string ThankyouGet = "GetThankyou";

        private readonly IEnrolmentContext _context;

        /// <summary>
        /// Initialises a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="context"><see cref="IEnrolmentContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public HomeController(IEnrolmentContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the home page.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
        public IActionResult Index()
        {
            return this.RedirectToAction(StudentDetailsGet);
        }

        /// <summary>
        /// Clears the enrolment form cookies.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
        [Route("start-over")]
        [HttpGet]
        public async Task<IActionResult> ClearEnrolmentForm()
        {
            await this._context.CookieHelper.ClearFormIdAsync(this).ConfigureAwait(false);

            return this.RedirectToAction(StudentDetailsGet);
        }

        /// <summary>
        /// Gets the student details page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("student-details")]
        [HttpGet]
        public async Task<IActionResult> GetStudentDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.StudentDetailsService.GetStudentDetailsAsync(formId).ConfigureAwait(false) ??
                        new StudentDetailsViewModel();

            return this.View("StudentDetails", model.Clone());
        }

        /// <summary>
        /// Sets the student details.
        /// </summary>
        /// <param name="model"><see cref="StudentDetailsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.StudentDetailsService.SaveStudentDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            return this.RedirectToAction(GuardianDetailsGet);
        }

        /// <summary>
        /// Gets the parent/guardian details page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("guardian-details")]
        [HttpGet]
        public async Task<IActionResult> GetGuardianDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.GuardianDetailsService.GetGuardianDetailsAsync(formId).ConfigureAwait(false) ??
                        new GuardianDetailsViewModel();

            return this.View("GuardianDetails", model.Clone());
        }

        /// <summary>
        /// Sets the parent/guardian details.
        /// </summary>
        /// <param name="model"><see cref="GuardianDetailsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.GuardianDetailsService.SaveGuardianDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? StudentDetailsGet
                                 : EmergencyContactDetailsGet;

            return this.RedirectToAction(actionName);
        }

        /// <summary>
        /// Gets the emergency contact details page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
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

        /// <summary>
        /// Sets the emergency contact details.
        /// </summary>
        /// <param name="model"><see cref="EmergencyContactDetailsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
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
                                 ? GuardianDetailsGet
                                 : MedicalDetailsGet;

            return this.RedirectToAction(actionName);
        }

        /// <summary>
        /// Gets the medical details page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("medical-details")]
        [HttpGet]
        public async Task<IActionResult> GetMedicalDetailsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.MedicalDetailsService.GetMedicalDetailsAsync(formId).ConfigureAwait(false) ??
                        new MedicalDetailsViewModel();

            return this.View("MedicalDetails", model.Clone());
        }

        /// <summary>
        /// Sets the medical details.
        /// </summary>
        /// <param name="model"><see cref="MedicalDetailsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.MedicalDetailsService.SaveMedicalDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? EmergencyContactDetailsGet
                                 : GuardianConsentsGet;

            return this.RedirectToAction(actionName);
        }

        /// <summary>
        /// Gets the parent/guardian consents page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("guardian-consents")]
        [HttpGet]
        public async Task<IActionResult> GetGuardianConsentsForm()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var model = await this._context.GuardianConsentsService.GetGuardianConsentsAsync(formId).ConfigureAwait(false) ??
                        new GuardianConsentsViewModel();

            return this.View("GuardianConsents", model.Clone());
        }

        /// <summary>
        /// Sets the parent/guardian consents.
        /// </summary>
        /// <param name="model"><see cref="GuardianConsentsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
        [Route("guardian-consents")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGuardianConsents(GuardianConsentsViewModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            this.CheckGuardianConsentsTicked(model);

            if (!ModelState.IsValid)
            {
                var vm = model.Clone();

                return this.View("GuardianConsents", vm);
            }

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.GuardianConsentsService.SaveGuardianConsentsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? MedicalDetailsGet
                                 : ConfirmationGet;

            return this.RedirectToAction(actionName);
        }

        /// <summary>
        /// Gets the conformation page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("confirmation")]
        [HttpGet]
        public async Task<IActionResult> GetConfirmation()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var vm = new ConfirmationViewModel()
                     {
                         StudentDetails = await this._context.StudentDetailsService.GetStudentDetailsAsync(formId).ConfigureAwait(false),
                         GuardianDetails = await this._context.GuardianDetailsService.GetGuardianDetailsAsync(formId).ConfigureAwait(false),
                         EmergencyContactDetails = await this._context.EmergencyContactDetailsService.GetEmergencyContactDetailsAsync(formId).ConfigureAwait(false),
                         MedicalDetails = await this._context.MedicalDetailsService.GetMedicalDetailsAsync(formId).ConfigureAwait(false),
                         GuardianConsents = await this._context.GuardianConsentsService.GetGuardianConsentsAsync(formId).ConfigureAwait(false),
                     };

            return this.View("GetConfirmation", vm);
        }

        /// <summary>
        /// Saves all details.
        /// </summary>
        /// <param name="model"><see cref="ConfirmationViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="IActionResult"/> instance for redirection.</returns>
        [Route("submit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEnrolment(ConfirmationViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase))
            {
                return this.RedirectToAction(GuardianConsentsGet);
            }

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var sd = await this._context.StudentDetailsService.GetStudentDetailsAsync(formId).ConfigureAwait(false);
            var gd = await this._context.GuardianDetailsService.GetGuardianDetailsAsync(formId).ConfigureAwait(false);

            var amount = await this._context.PaymentService.GetAmountAsync(sd.IsDomestic, sd.YearLevel, DateTimeOffset.UtcNow).ConfigureAwait(false);
            var referenceNumber = await this._context.PaymentService.SavePaymentAsync(formId, amount).ConfigureAwait(false);

            var template = await this._context.SendGridMailService.GetEmailTemplateAsync("SubmissionConfirmation").ConfigureAwait(false);

            var vm = new EmailViewModel()
                     {
                         Personalizations =
                         {
                             new Personalisation()
                             {
                                 To =
                                 {
                                     new MailAddress()
                                     {
                                         Name = $"{gd.FirstName} {gd.LastName}",
                                         Email = gd.Email
                                     }
                                 }
                             }
                         },
                         Subject = template.Subject.Replace(":name", $"{sd.FirstName} {sd.LastName}"),
                         Content =
                         {
                             new Content()
                             {
                                 Type = "text/plain", // TODO: implement enum
                                 Value =
                                     template.PlainContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                             },
                             new Content()
                             {
                                 Type = "text/html",
                                 Value =
                                     template.HtmlContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                             },
                         }
            };
            await this._context.SendGridMailService.SendAsync(vm).ConfigureAwait(false);

            return this.RedirectToAction(ThankyouGet);
        }

        /// <summary>
        /// Gets the thank you page of the enrolment form.
        /// </summary>
        /// <returns>Returns the <see cref="IActionResult"/> instance as a view.</returns>
        [Route("thankyou")]
        [HttpGet]
        public async Task<IActionResult> GetThankyou()
        {
            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            var rn = await this._context.PaymentService.GetReferenceNumberByFormIdAsync(formId).ConfigureAwait(false);
            var gd = await this._context.GuardianDetailsService.GetGuardianDetailsAsync(formId).ConfigureAwait(false);

            await this._context.CookieHelper.ClearFormIdAsync(this).ConfigureAwait(false);

            var vm = new ThankyouViewModel() { ReferenceNumber = rn, Email = gd.Email };

            return this.View("GetThankyou", vm);
        }

        private void CheckGuardianConsentsTicked(GuardianConsentsViewModel model)
        {
            if (!model.AgreeToc)
            {
                ModelState.AddModelError("AgreeToc", "ToC must be ticked");
            }

            if (!model.AgreePhoto)
            {
                ModelState.AddModelError("AgreePhoto", "Photo must be ticked");
            }

            if (!model.AgreeSms)
            {
                ModelState.AddModelError("AgreeSms", "SMS must be ticked");
            }

            if (!model.AgreeKakaoTalk)
            {
                ModelState.AddModelError("AgreeKakaoTalk", "KakaoTalk must be ticked");
            }
        }
    }
}