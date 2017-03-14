using System;

using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="AdminContext"/> class.
    /// </summary>
    public interface IAdminContext : IDisposable
    {

        /// <summary>
        /// Gets the <see cref="IPaymentService"/> instance.
        /// </summary>
        IPaymentService PaymentService { get; }

        /// <summary>
        /// Gets the <see cref="ISendGridMailService"/> instance.
        /// </summary>
        ISendGridMailService SendGridMailService { get; }

        /// <summary>
        /// Gets the <see cref="IDownloadService"/> instance.
        /// </summary>
        IDownloadService DownloadService { get; }
    }
}