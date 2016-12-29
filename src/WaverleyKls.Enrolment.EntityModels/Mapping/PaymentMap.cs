using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WaverleyKls.Enrolment.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Payment"/> class.
    /// </summary>
    public class PaymentMap : IEntityMapper<Payment>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EnrolmentFormMap"/> class.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{Payment}"/> instance.</param>
        public void Map(EntityTypeBuilder<Payment> builder)
        {
            // Primary Key
            builder.HasKey(p => p.PaymentId);

            // Properties
            builder.Property(p => p.PaymentId).IsRequired();
            builder.Property(p => p.FormId).IsRequired();
            builder.Property(p => p.ReferenceNumber).IsRequired().HasMaxLength(16);
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.DatePaid).IsRequired();
            builder.Property(p => p.DateCreated).IsRequired();
            builder.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            builder.ToTable("Payment");
            builder.Property(p => p.PaymentId).HasColumnName("PaymentId");
            builder.Property(p => p.FormId).HasColumnName("FormId");
            builder.Property(p => p.ReferenceNumber).HasColumnName("ReferenceNumber");
            builder.Property(p => p.Amount).HasColumnName("Amount");
            builder.Property(p => p.DatePaid).HasColumnName("DatePaid");
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated");
            builder.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            builder.HasOne(p => p.EnrolmentForm)
                   .WithMany(p => p.Payments)
                   .HasForeignKey(p => p.FormId);
        }
    }
}