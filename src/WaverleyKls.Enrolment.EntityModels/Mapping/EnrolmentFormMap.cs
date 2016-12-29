using Aliencube.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WaverleyKls.Enrolment.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="EnrolmentForm"/> class.
    /// </summary>
    public class EnrolmentFormMap : IEntityMapper<EnrolmentForm>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EnrolmentFormMap"/> class.
        /// </summary>
        /// <param name="builder"><see cref="EntityTypeBuilder{EnrolmentForm}"/> instance.</param>
        public void Map(EntityTypeBuilder<EnrolmentForm> builder)
        {
            // Primary Key
            builder.HasKey(p => p.FormId);

            // Properties
            builder.Property(p => p.FormId).IsRequired();
            builder.Property(p => p.StudentDetails).IsRequired(false).HasMaxLength(int.MaxValue);
            builder.Property(p => p.GuardianDetails).IsRequired(false).HasMaxLength(int.MaxValue);
            builder.Property(p => p.EmergencyContactDetails).IsRequired(false).HasMaxLength(int.MaxValue);
            builder.Property(p => p.MedicalDetails).IsRequired(false).HasMaxLength(int.MaxValue);
            builder.Property(p => p.GuardianConsents).IsRequired(false).HasMaxLength(int.MaxValue);
            builder.Property(p => p.DateCreated).IsRequired();
            builder.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            builder.ToTable("EnrolmentForm");
            builder.Property(p => p.FormId).HasColumnName("FormId");
            builder.Property(p => p.StudentDetails).HasColumnName("StudentDetails");
            builder.Property(p => p.GuardianDetails).HasColumnName("GuardianDetails");
            builder.Property(p => p.EmergencyContactDetails).HasColumnName("EmergencyContactDetails");
            builder.Property(p => p.MedicalDetails).HasColumnName("MedicalDetails");
            builder.Property(p => p.GuardianConsents).HasColumnName("GuardianConsents");
            builder.Property(p => p.DateCreated).HasColumnName("DateCreated");
            builder.Property(p => p.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}