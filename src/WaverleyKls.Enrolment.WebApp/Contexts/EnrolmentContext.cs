using System;

using WaverleyKls.Enrolment.Helpers.Interfaces;
using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    /// <summary>
    /// This represents the context entity for enrolment form.
    /// </summary>
    public class EnrolmentContext : IEnrolmentContext
    {
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="EnrolmentContext"/> class.
        /// </summary>
        /// <param name="cookieHelper"><see cref="ICookieHelper"/> instance.</param>
        /// <param name="studentDetailsService"><see cref="IStudentDetailsService"/> instance.</param>
        /// <param name="guardianDetailsService"><see cref="IGuardianDetailsService"/> instance.</param>
        /// <param name="emergencyContactDetailsService"><see cref="IEmergencyContactDetailsService"/> instance.</param>
        /// <param name="medicalDetailsService"><see cref="IMedicalDetailsService"/> instance.</param>
        /// <param name="guardianConsentsService"><see cref="IGuardianConsentsService"/> instance.</param>
        /// <param name="paymentService"><see cref="IPaymentService"/> instance.</param>
        /// <param name="sendGridMailService"><see cref="ISendGridMailService"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cookieHelper"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="studentDetailsService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="guardianDetailsService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="emergencyContactDetailsService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="medicalDetailsService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="guardianConsentsService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="paymentService"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="sendGridMailService"/> is <see langword="null" />.</exception>
        public EnrolmentContext(ICookieHelper cookieHelper,
                                IStudentDetailsService studentDetailsService,
                                IGuardianDetailsService guardianDetailsService,
                                IEmergencyContactDetailsService emergencyContactDetailsService,
                                IMedicalDetailsService medicalDetailsService,
                                IGuardianConsentsService guardianConsentsService,
                                IPaymentService paymentService,
                                ISendGridMailService sendGridMailService)
        {
            if (cookieHelper == null)
            {
                throw new ArgumentNullException(nameof(cookieHelper));
            }

            this.CookieHelper = cookieHelper;

            if (studentDetailsService == null)
            {
                throw new ArgumentNullException(nameof(studentDetailsService));
            }

            this.StudentDetailsService = studentDetailsService;

            if (guardianDetailsService == null)
            {
                throw new ArgumentNullException(nameof(guardianDetailsService));
            }

            this.GuardianDetailsService = guardianDetailsService;

            if (emergencyContactDetailsService == null)
            {
                throw new ArgumentNullException(nameof(emergencyContactDetailsService));
            }

            this.EmergencyContactDetailsService = emergencyContactDetailsService;

            if (medicalDetailsService == null)
            {
                throw new ArgumentNullException(nameof(medicalDetailsService));
            }

            this.MedicalDetailsService = medicalDetailsService;

            if (guardianConsentsService == null)
            {
                throw new ArgumentNullException(nameof(guardianConsentsService));
            }

            this.GuardianConsentsService = guardianConsentsService;

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
        }

        /// <summary>
        /// Gets the <see cref="ICookieHelper"/> instance.
        /// </summary>
        public ICookieHelper CookieHelper { get; }

        /// <summary>
        /// Gets the <see cref="IStudentDetailsService"/> instance.
        /// </summary>
        public IStudentDetailsService StudentDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IGuardianDetailsService"/> instance.
        /// </summary>
        public IGuardianDetailsService GuardianDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IEmergencyContactDetailsService"/> instance.
        /// </summary>
        public IEmergencyContactDetailsService EmergencyContactDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IMedicalDetailsService"/> instance.
        /// </summary>
        public IMedicalDetailsService MedicalDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IGuardianConsentsService"/> instance.
        /// </summary>
        public IGuardianConsentsService GuardianConsentsService { get; }

        /// <summary>
        /// Gets the <see cref="IPaymentService"/> instance.
        /// </summary>
        public IPaymentService PaymentService { get; }

        /// <summary>
        /// Gets the <see cref="ISendGridMailService"/> instance.
        /// </summary>
        public ISendGridMailService SendGridMailService { get; }

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
