using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="PaymentService"/> class.
    /// </summary>
    public interface IPaymentService : IDisposable
    {
        /// <summary>
        /// Gets the reference number based on the enrolment for Id.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the reference number.</returns>
        Task<string> GetReferenceNumberByFormIdAsync(Guid formId);

        /// <summary>
        /// Gets the payment amount based on residential status, year level and date of submission.
        /// </summary>
        /// <param name="isDomestic">Value to specify whether the student applies domestic fee or not.</param>
        /// <param name="yearLevel">Student's year level.</param>
        /// <param name="now">Date of submission.</param>
        /// <returns>Returns the payment amount.</returns>
        Task<decimal> GetAmountAsync(bool isDomestic, string yearLevel, DateTimeOffset now);

        /// <summary>
        /// Saves the payment details into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="lastName">Student's last name.</param>
        /// <param name="amount">Payment amount.</param>
        /// <returns>Returns the reference number.</returns>
        Task<string> SavePaymentAsync(Guid formId, string lastName, decimal amount);

        /// <summary>
        /// Gets the list of payment details.
        /// </summary>
        /// <param name="yearLevel">Year level value. Default is <c>All</c>.</param>
        /// <param name="includePaid">Value that specifies whether to include paid enrolment or not. Default is <c>False</c>.</param>
        /// <returns>Returns the list of payment details.</returns>
        Task<PaymentViewModel> GetPaymentsAsync(string yearLevel = "all", bool includePaid = false);

        /// <summary>
        /// Saves the payment status.
        /// </summary>
        /// <param name="paymentId">Payment Id.</param>
        /// <param name="isPaid">Value that specifies whether the payment was made or not.</param>
        /// <returns>Returns the <see cref="PaymentStatusViewModel"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid enrolment form Id.</exception>
        Task<PaymentStatusViewModel> SavePaymentStatusAsync(Guid paymentId, bool isPaid);
    }
}