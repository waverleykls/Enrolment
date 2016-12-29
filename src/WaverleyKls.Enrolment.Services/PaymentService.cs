using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Services.Interfaces;

namespace WaverleyKls.Enrolment.Services
{
    /// <summary>
    /// This represents the service entity for payment.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private const decimal KinderAmount = 300.00M;
        private const decimal FullFeeAmount = 300.00M;
        private const decimal DomesticAmount = 95.00M;
        private const decimal SecondTermAmount = 300.00M;
        private const decimal ThirdTermAmount = 200.00M;
        private const decimal FourthTermAmount = 100.00M;

        private const string AEST = "AUS Eastern Standard Time";

        private readonly IWklsDbContext _context;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="MedicalDetailsService"/> class.
        /// </summary>
        /// <param name="context"><see cref="IWklsDbContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public PaymentService(IWklsDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the reference number based on the enrolment for Id.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the reference number.</returns>
        /// <exception cref="ArgumentException">Invalid enrolment form Id.</exception>
        public async Task<string> GetReferenceNumberByFormIdAsync(Guid formId)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid enrolment form Id", nameof(formId));
            }

            var payment = await this._context.Payments.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);

            return payment?.ReferenceNumber;
        }

        /// <summary>
        /// Gets the payment amount based on residential status, year level and date of submission.
        /// </summary>
        /// <param name="isDomestic">Value to specify whether the student applies domestic fee or not.</param>
        /// <param name="yearLevel">Student's year level.</param>
        /// <param name="now">Date of submission.</param>
        /// <returns>Returns the payment amount.</returns>
        /// <exception cref="ArgumentException">Invalid year level.</exception>
        public async Task<decimal> GetAmountAsync(bool isDomestic, string yearLevel, DateTimeOffset now)
        {
            if (!IsValidYearLevel(yearLevel))
            {
                throw new ArgumentException("Invalid year level", nameof(yearLevel));
            }

            var amount = await Task.Factory.StartNew(() => GetAmount(isDomestic, yearLevel, now)).ConfigureAwait(false);

            return amount;
        }

        /// <summary>
        /// Saves the payment details into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="amount">Payment amount.</param>
        /// <returns>Returns the reference number.</returns>
        /// <exception cref="ArgumentException">Invalid enrolment form Id.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Invalid amount.</exception>
        public async Task<string> SavePaymentAsync(Guid formId, decimal amount)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid enrolment form Id", nameof(formId));
            }

            if (!IsValidAmount(amount))
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Invalid amount");
            }

            var payment = await this.AddOrUpdatePaymentAsync(formId, amount).ConfigureAwait(false);

            var transaction = await this._context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                await this._context.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return payment.ReferenceNumber;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

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

        private static decimal GetAmount(bool isDomestic, string yearLevel, DateTimeOffset now)
        {
            if (!isDomestic)
            {
                return FullFeeAmount;
            }

            if (yearLevel == "K")
            {
                return KinderAmount;
            }

            var adjusted = AdjustTimeZone(now);
            if (IsInFirstTerm(adjusted))
            {
                return DomesticAmount;
            }

            if (IsInSecondTerm(adjusted))
            {
                return SecondTermAmount;
            }

            if (IsInThirdTerm(adjusted))
            {
                return ThirdTermAmount;
            }

            return FourthTermAmount;
        }

        private static DateTimeOffset AdjustTimeZone(DateTimeOffset now)
        {
            var tz = TimeZoneInfo.GetSystemTimeZones().Single(p => p.Id.Equals(AEST, StringComparison.CurrentCultureIgnoreCase));

            return now.ToOffset(tz.IsDaylightSavingTime(now) ? tz.BaseUtcOffset.Add(new TimeSpan(0, 1, 0, 0)) : tz.BaseUtcOffset);
        }

        private static bool IsInFirstTerm(DateTimeOffset now)
        {
            var firstTermEnd = new DateTimeOffset(2017, 4, 1, 0, 0, 0, new TimeSpan(0, 11, 0, 0));

            return now < firstTermEnd;
        }

        private static bool IsInSecondTerm(DateTimeOffset now)
        {
            var secondTermEnd = new DateTimeOffset(2017, 7, 1, 0, 0, 0, new TimeSpan(0, 10, 0, 0));

            return now < secondTermEnd;
        }

        private static bool IsInThirdTerm(DateTimeOffset now)
        {
            var thirdTermEnd = new DateTimeOffset(2017, 9, 23, 0, 0, 0, new TimeSpan(0, 10, 0, 0));

            return now < thirdTermEnd;
        }

        private static bool IsInFourthTerm(DateTimeOffset now)
        {
            var fourthTermEnd = new DateTimeOffset(2017, 12, 22, 0, 0, 0, new TimeSpan(0, 11, 0, 0));

            return now < fourthTermEnd;
        }

        private static bool IsValidAmount(decimal amount)
        {
            var valid = amount == FullFeeAmount || amount == KinderAmount || amount == DomesticAmount ||
                        amount == SecondTermAmount || amount == ThirdTermAmount || amount == FourthTermAmount;

            return valid;
        }

        private static bool IsValidYearLevel(string yearLevel)
        {
            var levels = new[] { "K", "P", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

            var valid = levels.Any(p => p.Equals(yearLevel, StringComparison.CurrentCultureIgnoreCase));

            return valid;
        }

        private async Task<Payment> AddOrUpdatePaymentAsync(Guid formId, decimal amount)
        {
            var now = DateTimeOffset.UtcNow;

            var payment = await this._context.Payments.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (payment == null)
            {
                payment = new Payment() { PaymentId = Guid.NewGuid(), FormId = formId, DateCreated = now };
            }

            var adjusted = AdjustTimeZone(now);
            var count = await this._context.Payments.CountAsync().ConfigureAwait(false);

            payment.Amount = amount;
            payment.ReferenceNumber = $"WKLS{adjusted.Year}{adjusted.Month:00}{count + 1:0000}";
            payment.DateUpdated = now;

            this._context.AddOrUpdate(payment);

            return payment;
        }
    }
}