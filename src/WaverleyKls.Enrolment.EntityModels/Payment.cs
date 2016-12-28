using System;

namespace WaverleyKls.Enrolment.EntityModels
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid FormId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset DatePaid { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }

        public virtual EnrolmentForm EnrolmentForm { get; set; }
    }
}