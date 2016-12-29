using System;

using WaverleyKls.Enrolment.Helpers.Interfaces;
using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    public interface IEnrolmentContext : IDisposable
    {
        ICookieHelper CookieHelper { get; }
        IStudentDetailsService StudentDetailsService { get; }
        IGuardianDetailsService GuardianDetailsService { get; }
        IEmergencyContactDetailsService EmergencyContactDetailsService { get; }
        IMedicalDetailsService MedicalDetailsService { get; }
        IGuardianConsentsService GuardianConsentsService { get; }
        IPaymentService PaymentService { get; }

        ISendGridMailService SendGridMailService { get; }
    }
}