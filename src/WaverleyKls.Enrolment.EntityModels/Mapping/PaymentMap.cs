using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WaverleyKls.Enrolment.EntityModels.Mapping
{
    public class PaymentMap : IEntityMapper<Payment>
    {
        public void Map(EntityTypeBuilder<Payment> builder)
        {
            // Primary Key
            builder.HasKey(p => p.PaymentId);

            // Properties
            builder.Property(p => p.PaymentId).IsRequired();
            builder.Property(p => p.FormId).IsRequired();
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.DatePaid).IsRequired();
            builder.Property(p => p.DateCreated).IsRequired();
            builder.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            builder.ToTable("Payment");
            builder.Property(p => p.PaymentId).HasColumnName("PaymentId");
            builder.Property(p => p.FormId).HasColumnName("FormId");
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