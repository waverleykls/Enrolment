using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for medical details page.
    /// </summary>
    public class MedicalDetailsViewModel : IInitialisable, ICloneable<MedicalDetailsViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MedicalDetailsViewModel"/> class.
        /// </summary>
        public MedicalDetailsViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="MedicalDetailsViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="MedicalDetailsViewModel"/> instance.</param>
        /// <param name="initialise">Value that indicates whether to initialise or not.</param>
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

        /// <summary>
        /// Gets or sets the list of answers about medical concerns.
        /// </summary>
        public List<SelectListItem> MedicalConcerns { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the applicant has a medical concern or not.
        /// </summary>
        public bool HasAnyMedicalConcern { get; set; }

        /// <summary>
        /// Gets or sets the medical concern details.
        /// </summary>
        //[RegularExpression(@"[\w\s]+")]
        public string MedicalConcernDetails { get; set; }

        /// <summary>
        /// Gets or sets the list of answers about medications.
        /// </summary>
        public List<SelectListItem> Medications { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the applicant is taking medications or not.
        /// </summary>
        public bool IsTakingMedication { get; set; }

        /// <summary>
        /// Gets or sets the medication details.
        /// </summary>
        //[RegularExpression(@"[\w\s]+")]
        public string MedicationDetails { get; set; }

        /// <summary>
        /// Gets or sets the direction for page navigation.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Initialises with pre-defined values.
        /// </summary>
        public void Initialise()
        {
            this.MedicalConcerns = CommonItemsGenerator.GetAnswers()
                                                       .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString().ToLowerInvariant(), Selected = false })
                                                       .ToList();

            this.Medications = CommonItemsGenerator.GetAnswers()
                                                   .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString().ToLowerInvariant(), Selected = false })
                                                   .ToList();
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        public MedicalDetailsViewModel Clone(bool initialise = true)
        {
            var vm = new MedicalDetailsViewModel(this, initialise);

            return vm;
        }
    }
}
