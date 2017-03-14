using System;

using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    /// <summary>
    /// This represents the context entity for the admin page.
    /// </summary>
    public class AdminContext : IAdminContext
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="AdminContext"/> class.
        /// </summary>
        /// <param name="paymentService"><see cref="IPaymentService"/> instance.</param>
        /// <param name="sendGridMailService"><see cref="ISendGridMailService"/> instance.</param>
        /// <param name="downloadService"><see cref="IDownloadService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="paymentService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="sendGridMailService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="downloadService"/> is <see langword="null" />.</exception>
        public AdminContext(IPaymentService paymentService, ISendGridMailService sendGridMailService, IDownloadService downloadService)
        {
            if (paymentService == null)
            {
                throw new ArgumentNullException(nameof(paymentService));
            }

            this.PaymentService = paymentService;

            if (sendGridMailService == null)
            {
                throw new ArgumentNullException(nameof(sendGridMailService));
            }

            this.SendGridMailService = sendGridMailService;

            if (downloadService == null)
            {
                throw new ArgumentNullException(nameof(downloadService));
            }

            this.DownloadService = downloadService;
        }

        /// <summary>
        /// Gets the <see cref="IPaymentService"/> instance.
        /// </summary>
        public IPaymentService PaymentService { get; }

        /// <summary>
        /// Gets the <see cref="ISendGridMailService"/> instance.
        /// </summary>
        public ISendGridMailService SendGridMailService { get; }

        /// <summary>
        /// Gets the <see cref="IDownloadService"/> instance.
        /// </summary>
        public IDownloadService DownloadService { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}
