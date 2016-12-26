using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class MedicalDetailsViewModel : IInitialisable, ICloneable<MedicalDetailsViewModel>
    {
        public MedicalDetailsViewModel()
        {
            this.Initialise();
        }

        private MedicalDetailsViewModel(MedicalDetailsViewModel model, bool initialise = true)
        {
            if (model == null)
            {
                return;
            }

            if (initialise)
            {
                this.Initialise();
            }

            this.HasAnyMedicalConcern = model.HasAnyMedicalConcern;
            this.MedicalConcernDetails = model.MedicalConcernDetails;
            this.IsTakingMedication = model.IsTakingMedication;
            this.MedicationDetails = model.MedicationDetails;
        }

        public List<SelectListItem> MedicalConcerns { get; set; }

        public bool HasAnyMedicalConcern { get; set; }

        public string MedicalConcernDetails { get; set; }

        public List<SelectListItem> Medications { get; set; }

        public bool IsTakingMedication { get; set; }

        public string MedicationDetails { get; set; }

        public string Direction { get; set; }
        public void Initialise()
        {
            this.MedicalConcerns = CommonItemsGenerator.GetAnswers()
                                                       .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString().ToLowerInvariant(), Selected = false })
                                                       .ToList();

            this.Medications = CommonItemsGenerator.GetAnswers()
                                                   .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString().ToLowerInvariant(), Selected = false })
                                                   .ToList();
        }

        public MedicalDetailsViewModel Clone(bool initialise = true)
        {
            var vm = new MedicalDetailsViewModel(this, initialise);

            return vm;
        }
    }
}
