using System;
using System.Collections.Generic;

namespace WaverleyKls.Enrolment.EntityModels
{
    public class EnrolmentForm
    {
        public Guid FormId { get; set; }
        public string StudentDetails { get; set; }
        public string GuardianDetails { get; set; }
        public string EmergencyContactDetails { get; set; }
        public string MedicalDetails { get; set; }
        public string GuardianConsents { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }

        public virtual List<Payment> Payments { get; set; }
    }
}