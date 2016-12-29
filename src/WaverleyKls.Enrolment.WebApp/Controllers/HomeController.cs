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

            return this.RedirectToAction(StudentDetailsGet);
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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.StudentDetailsService.SaveStudentDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            return this.RedirectToAction(GuardianDetailsGet);
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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.GuardianDetailsService.SaveGuardianDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? StudentDetailsGet
                                 : EmergencyContactDetailsGet;

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
                                 ? GuardianDetailsGet
                                 : MedicalDetailsGet;

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

            var formId = await this._context.CookieHelper.GetFormIdAsync(this).ConfigureAwait(false);

            await this._context.MedicalDetailsService.SaveMedicalDetailsAsync(formId, model.Clone(false)).ConfigureAwait(false);

            var actionName = model.Direction.Equals("prev", StringComparison.CurrentCultureIgnoreCase)
                                 ? EmergencyContactDetailsGet
                                 : GuardianConsentsGet;

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