using System;

using Newtonsoft.Json;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for the payment status.
    /// </summary>
    public class PaymentStatusViewModel
    {
        /// <summary>
        /// Gets or sets the payment Id.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether the payment was made or not.
        /// </summary>
        [JsonProperty("paid")]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment status was updated.
        /// </summary>
        public DateTimeOffset? DateUpdated { get; set; }
    }
}