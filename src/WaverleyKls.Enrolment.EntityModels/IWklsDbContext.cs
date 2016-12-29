using Microsoft.EntityFrameworkCore;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This provides interfaces to the <see cref="WklsDbContext"/> class.
    /// </summary>
    public interface IWklsDbContext : IDbContext
    {
        /// <summary>
        /// Gets or sets the set of <see cref="EnrolmentForm"/> entities.
        /// </summary>
        DbSet<EnrolmentForm> EnrolmentForms { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Payment"/> entities.
        /// </summary>
        DbSet<Payment> Payments { get; set; }
    }
}
