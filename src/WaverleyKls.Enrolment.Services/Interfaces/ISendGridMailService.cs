using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="SendGridMailService"/> class.
    /// </summary>
    public interface ISendGridMailService : IDisposable
    {
        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="templateName">Template name.</param>
        /// <returns>Returns the <see cref="EmailTemplateViewModel"/> instance.</returns>
        Task<EmailTemplateViewModel> GetEmailTemplateAsync(string templateName);

        /// <summary>
        /// Gets the <see cref="EmailViewModel"/> for confirmation to be sent to the applicant.
        /// </summary>
        /// <param name="sd"><see cref="StudentDetailsViewModel"/> object.</param>
        /// <param name="gd"><see cref="GuardianDetailsViewModel"/> object.</param>
        /// <param name="amount">Amount to pay.</param>
        /// <param name="referenceNumber">Reference number.</param>
        /// <param name="template"><see cref="EmailTemplateViewModel"/> object.</param>
        /// <returns>Returns the <see cref="EmailViewModel"/> for confirmation to be sent to the applicant.</returns>
        EmailViewModel GetConfirmationEmailViewModelForApplicant(StudentDetailsViewModel sd, GuardianDetailsViewModel gd, decimal amount, string referenceNumber, EmailTemplateViewModel template);

        /// <summary>
        /// Gets the <see cref="EmailViewModel"/> for confirmation to be sent to the admin.
        /// </summary>
        /// <param name="sd"><see cref="StudentDetailsViewModel"/> object.</param>
        /// <param name="gd"><see cref="GuardianDetailsViewModel"/> object.</param>
        /// <param name="amount">Amount to pay.</param>
        /// <param name="referenceNumber">Reference number.</param>
        /// <param name="template"><see cref="EmailTemplateViewModel"/> object.</param>
        /// <returns>Returns the <see cref="EmailViewModel"/> for confirmation to be sent to the admin.</returns>
        EmailViewModel GetConfirmationEmailViewModelForAdmin(StudentDetailsViewModel sd, GuardianDetailsViewModel gd, decimal amount, string referenceNumber, EmailTemplateViewModel template);

        /// <summary>
        /// Sends the email through SendGrid Web API.
        /// </summary>
        /// <param name="model"><see cref="EmailViewModel"/> instance.</param>
        Task SendAsync(EmailViewModel model);
    }
}