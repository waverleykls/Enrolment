using System;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This represents the database entity for payment.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Gets or sets the payment Id.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the enrolment form Id.
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the payment amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment was made.
        /// </summary>
        public DateTimeOffset DatePaid { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when the record was updated.
        /// </summary>
        public DateTimeOffset DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EntityModels.EnrolmentForm"/> instance.
        /// </summary>
        public virtual EnrolmentForm EnrolmentForm { get; set; }
    }
}