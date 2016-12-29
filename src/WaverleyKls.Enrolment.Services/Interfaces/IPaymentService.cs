using System;
using System.Threading.Tasks;

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
        /// <param name="amount">Payment amount.</param>
        /// <returns>Returns the reference number.</returns>
        Task<string> SavePaymentAsync(Guid formId, decimal amount);
    }
}