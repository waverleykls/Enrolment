using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WaverleyKls.Enrolment.EntityModels.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolmentForm",
                columns: table => new
                {
                    FormId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    EmergencyContactDetails = table.Column<string>(maxLength: 2147483647, nullable: true),
                    GuardianConsents = table.Column<string>(maxLength: 2147483647, nullable: true),
                    GuardianDetails = table.Column<string>(maxLength: 2147483647, nullable: true),
                    MedicalDetails = table.Column<string>(maxLength: 2147483647, nullable: true),
                    StudentDetails = table.Column<string>(maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentForm", x => x.FormId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolmentForm");
        }
    }
}
