using System;
using System.Collections.Generic;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This represents the database entity for enrolment form.
    /// </summary>
    public class EnrolmentForm
    {
        /// <summary>
        /// Gets or sets the form Id.
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the student details of the enrolment form.
        /// </summary>
        public string StudentDetails { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian details of the enrolment form.
        /// </summary>
        public string GuardianDetails { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact details of the enrolment form.
        /// </summary>
        public string EmergencyContactDetails { get; set; }

        /// <summary>
        /// Gets or sets the medical details of the enrolment form.
        /// </summary>
        public string MedicalDetails { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian consents of the enrolment form.
        /// </summary>
        public string GuardianConsents { get; set; }

        /// <summary>
        /// Gets or sets the date when the form is created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the form is updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the list of payment instances.
        /// </summary>
        public virtual List<Payment> Payments { get; set; }
    }
}