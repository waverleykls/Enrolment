using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WaverleyKls.Enrolment.EntityModels;

namespace WaverleyKls.Enrolment.EntityModels.Migrations
{
    [DbContext(typeof(WklsDbContext))]
    [Migration("20161225113120_InitialMigration")]
    partial class InitialMigration
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
        }
    }
}
