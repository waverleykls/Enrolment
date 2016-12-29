using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for student details page.
    /// </summary>
    public class StudentDetailsViewModel : IInitialisable, ICloneable<StudentDetailsViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="StudentDetailsViewModel"/> class.
        /// </summary>
        public StudentDetailsViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="StudentDetailsViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="StudentDetailsViewModel"/> instance.</param>
        /// <param name="initialise">Value that indicates whether to initialise or not.</param>
        private StudentDetailsViewModel(StudentDetailsViewModel model, bool initialise = true)
        {
            if (model == null)
            {
                return;
            }

            if (initialise)
            {
                this.Initialise();
            }

            this.FirstName = model.FirstName;
            this.MiddleNames = model.MiddleNames;
            this.LastName = model.LastName;
            this.Date = model.Date;
            this.Month = model.Month;
            this.Year = model.Year;
            this.Gender = model.Gender;
            this.Address1 = model.Address1;
            this.Address2 = model.Address2;
            this.Suburb = model.Suburb;
            this.State = model.State;
            this.Postcode = model.Postcode;
            this.SchoolName = model.SchoolName;
            this.YearLevel = model.YearLevel;
            this.IsDomestic = model.IsDomestic;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Display(Name = "First Name", Prompt = "First Name")]
        [Required]
        [RegularExpression(@"[a-zA-Z\-\' ]+")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle names.
        /// </summary>
        [Display(Name = "Middle Names", Prompt = "Middle Names")]
        [RegularExpression(@"[a-zA-Z\-\' ]+")]
        public string MiddleNames { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Display(Name = "Last Name", Prompt = "Last Name")]
        [Required]
        [RegularExpression(@"[a-zA-Z\-\' ]+")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the list of dates of a month.
        /// </summary>
        public List<SelectListItem> Dates { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        [Required]
        public int Date { get; set; }

        /// <summary>
        /// Gets or sets the list of months of a year.
        /// </summary>
        public List<SelectListItem> Months { get; set; }

        /// <summary>
        /// Gets or sets the month of birth.
        /// </summary>
        [Required]
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the list of years.
        /// </summary>
        public List<SelectListItem> Years { get; set; }

        /// <summary>
        /// Gets or sets the year of birth.
        /// </summary>
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the list of genders.
        /// </summary>
        [Display(Name = "Gender")]
        public List<SelectListItem> Genders { get; set; }

        /// <summary>
        /// Gets or sets the applicant's gender.
        /// </summary>
        [Required]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the address #1.
        /// </summary>
        [Display(Name = "Address #1", Prompt = "Address #1")]
        [Required]
        [RegularExpression(@"[\w\s]+")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address #2.
        /// </summary>
        [Display(Name = "Address #2", Prompt = "Address #2")]
        [RegularExpression(@"[\w\s]+")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        [Display(Name = "Suburb", Prompt = "Suburb")]
        [Required]
        [RegularExpression(@"[\w\s]+")]
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the list of suburbs.
        /// </summary>
        [Display(Name = "State")]
        public List<SelectListItem> States { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        [Required]
        [RegularExpression(@"(ACT|NT|NSW|QLD|SA|TAS|VIC|WA)")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        [Display(Name = "Postcode", Prompt = "Postcode")]
        [Required]
        [RegularExpression(@"[\d]{4,4}")]
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the applicant's school name.
        /// </summary>
        [Display(Name = "School Name", Prompt = "School Name: Student's mainstream school name")]
        [Required]
        [RegularExpression(@"[\w\s]+")]
        public string SchoolName { get; set; }

        /// <summary>
        /// Gets or sets the list of year levels.
        /// </summary>
        [Display(Name = "Year Level")]
        public List<SelectListItem> YearLevels { get; set; }

        /// <summary>
        /// Gets or sets the applicant's year level.
        /// </summary>
        [Required]
        [RegularExpression(@"(K|P|1|2|3|4|5|6|7|8|9|10|11|12)")]
        public string YearLevel { get; set; }

        /// <summary>
        /// Gets or sets the list of fee schemes.
        /// </summary>
        [Display(Name = "Residential Status")]
        public List<SelectListItem> FeeSchemes { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the fee scheme is for domestic students or not.
        /// </summary>
        [Required]
        public bool IsDomestic { get; set; }

        /// <summary>
        /// Initialises with pre-defined values.
        /// </summary>
        public void Initialise()
        {
            this.Dates = DateTimeItemsGenerator.GetDates()
                                              .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString() })
                                              .ToList();
            this.Dates.Insert(0, new SelectListItem() { Text = "Date", Value = string.Empty });

            this.Months = DateTimeItemsGenerator.GetMonths()
                                                .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString() })
                                                .ToList();
            this.Months.Insert(0, new SelectListItem() { Text = "Month", Value = string.Empty });

            this.Years = DateTimeItemsGenerator.GetYears()
                                               .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString() })
                                               .ToList();
            this.Years.Insert(0, new SelectListItem() { Text = "Year", Value = string.Empty });

            this.Genders = CommonItemsGenerator.GetGenders()
                                               .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value })
                                               .ToList();

            this.States = CommonItemsGenerator.GetStates()
                                              .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value, Selected = p.Value.Equals("VIC", StringComparison.CurrentCultureIgnoreCase) })
                                              .ToList();

            this.YearLevels = SchoolItemsGenerator.GetYearLevels()
                                                  .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value })
                                                  .ToList();
            this.YearLevels.Insert(0, new SelectListItem() { Text = "Year Level", Value = string.Empty });

            this.FeeSchemes = SchoolItemsGenerator.GetFeeSchemes()
                                                  .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString().ToLowerInvariant(), Selected = !p.Value })
                                                  .ToList();
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        public StudentDetailsViewModel Clone(bool initialise = true)
        {
            var vm = new StudentDetailsViewModel(this, initialise);

            return vm;
        }
    }
}