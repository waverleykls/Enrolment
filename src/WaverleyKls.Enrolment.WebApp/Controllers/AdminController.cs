using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.ViewModels;
using WaverleyKls.Enrolment.WebApp.Contexts;

namespace WaverleyKls.Enrolment.WebApp.Controllers
{
    /// <summary>
    /// This represents the controller entity for admin.
    /// </summary>
    [Route("admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="context"><see cref="IAdminContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public AdminController(IAdminContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the /admin/index page.
        /// </summary>
        /// <returns>Returns the /admin/index page.</returns>
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var yearLevelValue = this.Request.Query["yearLevel"].ToString();
            var yearLevel = yearLevelValue.IsNullOrWhiteSpace() ? "all" : yearLevelValue;

            bool result;
            var includePaid = bool.TryParse(this.Request.Query["includePaid"], out result) && result;

            var vm = await this._context.PaymentService.GetPaymentsAsync(yearLevel, includePaid).ConfigureAwait(false);
            vm.YearLevel = yearLevel;
            vm.IncludePaid = includePaid;

            return View(vm);
        }

        /// <summary>
        /// Updates the payment status.
        /// </summary>
        /// <param name="paymentId">Payment Id.</param>
        /// <param name="model"><see cref="PaymentStatusViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="PaymentStatusViewModel"/> instance.</returns>
        [Route("payments/{paymentId}")]
        [HttpPatch]
        public async Task<IActionResult> UpdatePaymentStatus(Guid paymentId, [FromBody] PaymentStatusViewModel model)
        {
            if (paymentId == Guid.Empty)
            {
                return BadRequest();
            }

            if (model == null)
            {
                return BadRequest();
            }

            var result = await this._context.PaymentService.SavePaymentStatusAsync(paymentId, model.IsPaid).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Downloads enrolment details.
        /// </summary>
        /// <returns>Returns the <see cref="DownloadViewModel"/> instance.</returns>
        [Route("download")]
        [HttpGet]
        public async Task<IActionResult> GetDownloadEnrolmentDetailsForm()
        {
            var vm = await this._context.DownloadService.GetDownloadableItemsAsync().ConfigureAwait(false);

            return this.View("DownloadEnrolmentDetails", vm);
        }

        /// <summary>
        /// Downloads enrolment details.
        /// </summary>
        /// <param name="model"><see cref="DownloadViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="DownloadViewModel"/> instance.</returns>
        [Route("download.csv")]
        [HttpPost]
        [Produces("text/csv")]
        public async Task<IActionResult> DownloadEnrolmentDetails(DownloadViewModel model)
        {
            if (model == null)
            {
                return this.BadRequest();
            }

            var result = await this._context.DownloadService.ProcessDownloadAsync(model).ConfigureAwait(false);
            return this.Ok(result);
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