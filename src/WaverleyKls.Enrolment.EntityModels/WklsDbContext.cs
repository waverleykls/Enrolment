using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

using WaverleyKls.Enrolment.EntityModels.Mapping;

namespace WaverleyKls.Enrolment.EntityModels
{
    /// <summary>
    /// This represents the database context entity for WKLS.
    /// </summary>
    public class WklsDbContext : DbContext, IWklsDbContext
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WklsDbContext"/> class.
        /// </summary>
        public WklsDbContext() : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="WklsDbContext"/> class.
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/> instance.</param>
        public WklsDbContext(DbContextOptions<WklsDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the set of <see cref="EnrolmentForm"/> entities.
        /// </summary>
        public DbSet<EnrolmentForm> EnrolmentForms { get; set; }

        /// <summary>
        /// Gets or sets the set of <see cref="Payment"/> entities.
        /// </summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Called while entity models are created.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EnrolmentForm>().Map(new EnrolmentFormMap());
            builder.Entity<Payment>().Map(new PaymentMap());
        }
    }
}
