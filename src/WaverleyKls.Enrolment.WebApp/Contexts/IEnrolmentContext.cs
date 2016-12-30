using System;

using WaverleyKls.Enrolment.Helpers.Interfaces;
using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    /// <summary>
    /// This provides interfaces to the <see cref="EnrolmentContext"/> class.
    /// </summary>
    public interface IEnrolmentContext : IDisposable
    {

        /// <summary>
        /// Gets the <see cref="ICookieHelper"/> instance.
        /// </summary>
        ICookieHelper CookieHelper { get; }

        /// <summary>
        /// Gets the <see cref="IStudentDetailsService"/> instance.
        /// </summary>
        IStudentDetailsService StudentDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IGuardianDetailsService"/> instance.
        /// </summary>
        IGuardianDetailsService GuardianDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IEmergencyContactDetailsService"/> instance.
        /// </summary>
        IEmergencyContactDetailsService EmergencyContactDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IMedicalDetailsService"/> instance.
        /// </summary>
        IMedicalDetailsService MedicalDetailsService { get; }

        /// <summary>
        /// Gets the <see cref="IGuardianConsentsService"/> instance.
        /// </summary>
        IGuardianConsentsService GuardianConsentsService { get; }

        /// <summary>
        /// Gets the <see cref="IPaymentService"/> instance.
        /// </summary>
        IPaymentService PaymentService { get; }

        /// <summary>
        /// Gets the <see cref="ISendGridMailService"/> instance.
        /// </summary>
        ISendGridMailService SendGridMailService { get; }
    }
}