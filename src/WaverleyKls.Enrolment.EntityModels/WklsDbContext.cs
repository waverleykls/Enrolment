using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

using WaverleyKls.Enrolment.EntityModels.Mapping;

namespace WaverleyKls.Enrolment.EntityModels
{
    public class WklsDbContext : DbContext, IWklsDbContext
    {
        public WklsDbContext() : base()
        {
        }

        public WklsDbContext(DbContextOptions<WklsDbContext> options) : base(options)
        {
        }

        public DbSet<EnrolmentForm> EnrolmentForms { get; set; }

        /// <summary>
        /// Called while entity models are created.
        /// </summary>
        /// <param name="builder"><see cref="ModelBuilder"/> instance.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EnrolmentForm>().Map(new EnrolmentFormMap());
        }
    }
}
