using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for parent/guardian details page.
    /// </summary>
    public class GuardianDetailsViewModel : IInitialisable, ICloneable<GuardianDetailsViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GuardianDetailsViewModel"/> class.
        /// </summary>
        public GuardianDetailsViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="GuardianDetailsViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="GuardianDetailsViewModel"/> instance.</param>
        /// <param name="initialise">Value that indicates whether to initialise or not.</param>
        private GuardianDetailsViewModel(GuardianDetailsViewModel model, bool initialise = true)
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
            this.RelationshipToStudent = model.RelationshipToStudent;
            this.HomePhone = model.HomePhone;
            this.WorkPhone = model.WorkPhone;
            this.MobilePhone = model.MobilePhone;
            this.Email = model.Email;
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
        /// Gets or sets the list of relationships to applicant.
        /// </summary>
        [Display(Name = "Relationship to Student")]
        public List<SelectListItem> Relationships { get; set; }

        /// <summary>
        /// Gets or sets the relationship to applicant.
        /// </summary>
        [Required]
        public string RelationshipToStudent { get; set; }

        /// <summary>
        /// Gets or sets the home phone number.
        /// </summary>
        [Display(Name = "Home Phone", Prompt = "Home Phone")]
        [RegularExpression(@"[\d\-\.\+ ]+")]
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the work phone number.
        /// </summary>
        [Display(Name = "Work Phone", Prompt = "Work Phone")]
        [RegularExpression(@"[\d\-\.\+ ]+")]
        public string WorkPhone { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone number.
        /// </summary>
        [Display(Name = "Mobile Phone", Prompt = "Mobile Phone")]
        [Required]
        [RegularExpression(@"[\d\-\.\+ ]+")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Display(Name = "Email", Prompt = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the direction for page navigation.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Initialises with pre-defined values.
        /// </summary>
        public void Initialise()
        {
            this.Relationships = ContactItemsGenerator.GetRelationships()
                                                      .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value })
                                                      .ToList();
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        public GuardianDetailsViewModel Clone(bool initialise = true)
        {
            var vm = new GuardianDetailsViewModel(this, initialise);

            return vm;
        }
    }
}
