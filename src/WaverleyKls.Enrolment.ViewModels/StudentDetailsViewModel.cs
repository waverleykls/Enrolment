using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class StudentDetailsViewModel
    {
        public StudentDetailsViewModel()
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

        public StudentDetailsViewModel(StudentDetailsViewModel model) : this()
        {
            if (model == null)
            {
                return;
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

        [Display(Name = "First Name", Prompt = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle Names", Prompt = "Middle Names")]
        public string MiddleNames { get; set; }

        [Display(Name = "Last Name", Prompt = "Last Name")]
        [Required]
        public string LastName { get; set; }

        public List<SelectListItem> Dates { get; set; }

        [Required]
        public int Date { get; set; }

        public List<SelectListItem> Months { get; set; }

        [Required]
        public int Month { get; set; }

        public List<SelectListItem> Years { get; set; }

        [Required]
        public int Year { get; set; }

        [Display(Name = "Gender")]
        public List<SelectListItem> Genders { get; set; }

        [Required]
        public string Gender { get; set; }

        [Display(Name = "Address #1", Prompt = "Address #1")]
        [Required]
        public string Address1 { get; set; }

        [Display(Name = "Address #2", Prompt = "Address #2")]
        public string Address2 { get; set; }

        [Display(Name = "Suburb", Prompt = "Suburb")]
        [Required]
        public string Suburb { get; set; }

        [Display(Name = "State")]
        [Required]
        public List<SelectListItem> States { get; set; }

        public string State { get; set; }

        [Display(Name = "Postcode", Prompt = "Postcode")]
        [Required]
        public string Postcode { get; set; }

        [Display(Name = "School Name", Prompt = "School Name: Student's mainstream school name")]
        [Required]
        public string SchoolName { get; set; }

        [Display(Name = "Year Level")]
        public List<SelectListItem> YearLevels { get; set; }

        [Required]
        public string YearLevel { get; set; }

        [Display(Name = "Residential Status")]
        public List<SelectListItem> FeeSchemes { get; set; }

        [Required]
        public bool IsDomestic { get; set; }
    }
}