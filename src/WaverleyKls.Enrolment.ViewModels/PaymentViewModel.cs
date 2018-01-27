using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for payment.
    /// </summary>
    public class PaymentViewModel: IInitialisable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PaymentViewModel"/> class.
        /// </summary>
        public PaymentViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Gets or sets the list of year levels.
        /// </summary>
        [Display(Name = "Year Level")]
        public List<SelectListItem> YearLevels { get; set; }

        /// <summary>
        /// Gets or sets the applicant's year level.
        /// </summary>
        public string YearLevel { get; set; }

        /// <summary>
        /// Gets or sets the value that specifies whether to include paid enrolment or not.
        /// </summary>
        public bool IncludePaid { get; set; }

        /// <summary>
        /// Gets or sets the list of payment model.
        /// </summary>
        public List<PaymentModel> Payments { get; set; }

        /// <summary>
        /// Initialises with pre-defined values.
        /// </summary>
        public void Initialise()
        {

            this.YearLevels = SchoolItemsGenerator.GetYearLevels()
                                                  .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value })
                                                  .ToList();
            this.YearLevels.Insert(0, new SelectListItem() { Text = "All Years", Value = string.Empty });
        }
    }

    /// <summary>
    /// This represents the data transfer object entity for payment.
    /// </summary>
    public class PaymentModel
    {
        /// <summary>
        /// Gets or sets the payment Id.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the student name.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Gets or sets the year level.
        /// </summary>
        public string YearLevel { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian name.
        /// </summary>
        public string GuardianName { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian's phone number
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// Gets or sets the parent/guardian's email.
        /// </summary>
        public string GuardianEmail { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the payment was made or not.
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Gets or sets the date when the enrolment record was created.
        /// </summary>
        public DateTimeOffset DateEnrolled { get; set; }

        /// <summary>
        /// Gets or sets the date when the payment was confirmed.
        /// </summary>
        public DateTimeOffset? DateConfirmed { get; set; }
    }
}