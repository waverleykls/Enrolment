using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class GuardianDetailsViewModel : IInitialisable, ICloneable<GuardianDetailsViewModel>
    {
        public GuardianDetailsViewModel()
        {
            this.Initialise();
        }

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

        [Display(Name = "First Name", Prompt = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle Names", Prompt = "Middle Names")]
        public string MiddleNames { get; set; }

        [Display(Name = "Last Name", Prompt = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Relationship to Student")]
        public List<SelectListItem> Relationships { get; set; }

        [Required]
        public string RelationshipToStudent { get; set; }

        [Display(Name = "Home Phone", Prompt = "Home Phone")]
        public string HomePhone { get; set; }

        [Display(Name = "Work Phone", Prompt = "Work Phone")]
        public string WorkPhone { get; set; }

        [Display(Name = "Mobile Phone", Prompt = "Mobile Phone")]
        [Required]
        public string MobilePhone { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        [Required]
        public string Email { get; set; }

        public string Direction { get; set; }

        public void Initialise()
        {
            this.Relationships = ContactItemsGenerator.GetRelationships()
                                                      .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value })
                                                      .ToList();
        }

        public GuardianDetailsViewModel Clone(bool initialise = true)
        {
            var vm = new GuardianDetailsViewModel(this, initialise);

            return vm;
        }
    }
}
