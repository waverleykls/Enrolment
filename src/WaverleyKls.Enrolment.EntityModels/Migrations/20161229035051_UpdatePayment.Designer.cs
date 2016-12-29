using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WaverleyKls.Enrolment.EntityModels;

namespace WaverleyKls.Enrolment.EntityModels.Migrations
{
    [DbContext(typeof(WklsDbContext))]
    [Migration("20161229035051_UpdatePayment")]
    partial class UpdatePayment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WaverleyKls.Enrolment.EntityModels.EnrolmentForm", b =>
                {
                    b.Property<Guid>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("FormId");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnName("DateCreated");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnName("DateUpdated");

                    b.Property<string>("EmergencyContactDetails")
                        .HasColumnName("EmergencyContactDetails")
                        .HasAnnotation("MaxLength", 2147483647);

                    b.Property<string>("GuardianConsents")
                        .HasColumnName("GuardianConsents")
                        .HasAnnotation("MaxLength", 2147483647);

                    b.Property<string>("GuardianDetails")
                        .HasColumnName("GuardianDetails")
                        .HasAnnotation("MaxLength", 2147483647);

                    b.Property<string>("MedicalDetails")
                        .HasColumnName("MedicalDetails")
                        .HasAnnotation("MaxLength", 2147483647);

                    b.Property<string>("StudentDetails")
                        .HasColumnName("StudentDetails")
                        .HasAnnotation("MaxLength", 2147483647);

                    b.HasKey("FormId");

                    b.ToTable("EnrolmentForm");
                });

            modelBuilder.Entity("WaverleyKls.Enrolment.EntityModels.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentId");

                    b.Property<decimal>("Amount")
                        .HasColumnName("Amount");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnName("DateCreated");

                    b.Property<DateTimeOffset>("DatePaid")
                        .HasColumnName("DatePaid");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnName("DateUpdated");

                    b.Property<Guid>("FormId")
                        .HasColumnName("FormId");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasColumnName("ReferenceNumber")
                        .HasAnnotation("MaxLength", 16);

                    b.HasKey("PaymentId");

                    b.HasIndex("FormId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("WaverleyKls.Enrolment.EntityModels.Payment", b =>
                {
                    b.HasOne("WaverleyKls.Enrolment.EntityModels.EnrolmentForm", "EnrolmentForm")
                        .WithMany("Payments")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
