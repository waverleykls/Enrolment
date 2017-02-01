using System;
using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for payment.
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Gets or sets the list of payment model.
        /// </summary>
        public List<PaymentModel> Payments { get; set; }
    }

    /// <summary>
    /// This represents the data transfer object entity for payment.
    /// </summary>
    public class PaymentModel
    {
        /// <summary>
        /// Gets or sets the payment Id.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the student name.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian name.
        /// </summary>
        public string GuardianName { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian's phone number
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian's email.
        /// </summary>
        public string GuardianEmail { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the payment was made or not.
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment was confirmed.
        /// </summary>
        public DateTimeOffset? DateConfirmed { get; set; }
    }
}