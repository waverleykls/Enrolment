using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

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
        /// Initialises a new instance of the <see cref="AdminController"/> class.
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
            bool result;
            var showAll = bool.TryParse(this.Request.Query["showAll"], out result) && result;

            var vm = await this._context.PaymentService.GetPaymentsAsync(showAll).ConfigureAwait(false);

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