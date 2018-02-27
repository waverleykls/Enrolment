using System;
using System.Collections.Generic;

using WaverleyKls.Enrolment.Extensions;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for downloadable details.
    /// </summary>
    public class DownloadableViewModel : IInitialisable
    {
        private readonly DownloadViewModel _dm;
        private readonly StudentDetailsViewModel _sd;
        private readonly GuardianDetailsViewModel _gd;
        private readonly bool _hasPaid;
        private readonly DateTimeOffset _dateEnrolled;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadableViewModel"/> class.
        /// </summary>
        /// <param name="dm"><see cref="DownloadViewModel"/> instance.</param>
        /// <param name="sd"><see cref="StudentDetailsViewModel"/> instance.</param>
        /// <param name="gd"><see cref="GuardianDetailsViewModel"/> instance.</param>
        /// <param name="hasPaid">Value indicating whether the payment has been made or not.</param>
        /// <param name="dateEnrolled">Date when the student has enrolled.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dm"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="sd"/> is <see langword="null"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="gd"/> is <see langword="null"/></exception>
        public DownloadableViewModel(DownloadViewModel dm, StudentDetailsViewModel sd, GuardianDetailsViewModel gd, bool hasPaid, DateTimeOffset dateEnrolled)
        {
            if (dm == null)
            {
                throw new ArgumentNullException(nameof(dm));
            }

            this._dm = dm;

            if (sd == null)
            {
                throw new ArgumentNullException(nameof(sd));
            }

            this._sd = sd;

            if (gd == null)
            {
                throw new ArgumentNullException(nameof(gd));
            }

            this._gd = gd;

            this._hasPaid = hasPaid;

            this._dateEnrolled = dateEnrolled;

            this.Initialise();
        }

        /// <summary>
        /// Gets or sets the student name.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Gets or sets the student school.
        /// </summary>
        public string StudentSchool { get; set; }

        /// <summary>
        /// Gets or sets the student year level.
        /// </summary>
        public string StudentYear { get; set; }

        /// <summary>
        /// Gets or sets the student gender.
        /// </summary>
        public string StudentGender { get; set; }

        /// <summary>
        /// Gets or sets the student date of birth.
        /// </summary>
        public string StudentDateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the student address.
        /// </summary>
        public string StudentAddress { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian name.
        /// </summary>
        public string GuardianName { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian contact number.
        /// </summary>
        public string GuardianContact { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian email.
        /// </summary>
        public string GuardianEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to have paid or not.
        /// </summary>
        public bool HasPaid { get; set; }

        /// <summary>
        /// Gets or sets the date/time when the student has enrolled.
        /// </summary>
        public DateTimeOffset DateEnrolled { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="DownloadableViewModel"/> class.
        /// </summary>
        public void Initialise()
        {
            var items = ResolveSelectedItems(this._dm);

            if (items.ContainsKey("sname") && items["sname"])
            {
                this.StudentName = $"{this._sd.FirstName} {this._sd.MiddleNames} {this._sd.LastName}";
            }

            if (items.ContainsKey("dob") && items["dob"])
            {
                this.StudentDateOfBirth = $"{this._sd.Date}/{this._sd.Month}/{this._sd.Year}";
            }

            if (items.ContainsKey("gender") && items["gender"])
            {
                this.StudentGender = this._sd.Gender;
            }

            if (items.ContainsKey("school") && items["school"])
            {
                this.StudentSchool = this._sd.SchoolName;
            }

            if (items.ContainsKey("year") && items["year"])
            {
                this.StudentYear = this._sd.YearLevel;
            }

            if (items.ContainsKey("address") && items["address"])
            {
                this.StudentAddress = $"{this._sd.Address1} {this._sd.Address2}, {this._sd.Suburb}, {this._sd.State}, {this._sd.Postcode}";
            }

            if (items.ContainsKey("gname") && items["gname"])
            {
                this.GuardianName = $"{this._gd.FirstName} {this._gd.MiddleNames} {this._gd.LastName}";
            }

            if (items.ContainsKey("phone") && items["phone"])
            {
                //this.GuardianContact = this._gd.MobilePhone;
                this.GuardianContact = !this._gd.MobilePhone.IsNullOrWhiteSpace()
                                           ? this._gd.MobilePhone.ToMobile()
                                           : !this._gd.WorkPhone.IsNullOrWhiteSpace()
                                               ? this._gd.WorkPhone.ToPhone()
                                               : this._gd.HomePhone.ToPhone();
            }

            if (items.ContainsKey("email") && items["email"])
            {
                this.GuardianEmail = this._gd.Email;
            }

            this.HasPaid = this._hasPaid;

            this.DateEnrolled = this._dateEnrolled;
        }

        private static Dictionary<string, bool> ResolveSelectedItems(DownloadViewModel dm)
        {
            var items = new Dictionary<string, bool>();

            for (var i = 0; i < dm.StudentDetails.Count; i++)
            {
                var item = dm.StudentDetails[i];
                items.Add(item.Value, dm.StudentDetailsSelected[i]);
            }

            for (var i = 0; i < dm.GuardianDetails.Count; i++)
            {
                var item = dm.GuardianDetails[i];
                items.Add(item.Value, dm.GuardianDetailsSelected[i]);
            }

            return items;
        }
    }
}