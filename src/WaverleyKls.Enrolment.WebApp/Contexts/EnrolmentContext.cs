using System;

using WaverleyKls.Enrolment.Helpers;
using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.WebApp.Contexts
{
    public class EnrolmentContext : IEnrolmentContext
    {
        private bool _disposed;

        public EnrolmentContext(ICookieHelper cookieHelper,
                                IStudentDetailsService studentDetailsService,
                                IGuardianDetailsService guardianDetailsService,
                                IEmergencyContactDetailsService emergencyContactDetailsService,
                                IMedicalDetailsService medicalDetailsService,
                                IGuardianConsentsService guardianConsentsService,
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

            if (sendGridMailService == null)
            {
                throw new ArgumentNullException(nameof(sendGridMailService));
            }

            this.SendGridMailService = sendGridMailService;
        }
        public ICookieHelper CookieHelper { get; }

        public IStudentDetailsService StudentDetailsService { get; }

        public IGuardianDetailsService GuardianDetailsService { get; }

        public IEmergencyContactDetailsService EmergencyContactDetailsService { get; }

        public IMedicalDetailsService MedicalDetailsService { get; }

        public IGuardianConsentsService GuardianConsentsService { get; }

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
