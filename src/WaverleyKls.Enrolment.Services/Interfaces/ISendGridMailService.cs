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
        /// Sends the email through SendGrid Web API.
        /// </summary>
        /// <param name="model"><see cref="EmailViewModel"/> instance.</param>
        Task SendAsync(EmailViewModel model);
    }
}