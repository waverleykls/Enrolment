using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface ISendGridMailService : IDisposable
    {
        Task<EmailTemplateViewModel> GetEmailTemplateAsync(string templateName);

        Task SendAsync(EmailViewModel model);
    }
}