using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Rendering;

using WaverleyKls.Enrolment.ViewModels.Generators;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for download enrolment details.
    /// </summary>
    public class DownloadViewModel : IInitialisable, ICloneable<DownloadViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadViewModel"/> class.
        /// </summary>
        public DownloadViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="DownloadViewModel"/> instance.</param>
        /// <param name="initialise">Value that indicates whether to initialise or not.</param>
        private DownloadViewModel(DownloadViewModel model, bool initialise = true)
        {
            if (model == null)
            {
                return;
            }

            if (initialise)
            {
                this.Initialise();
            }
        }

        /// <summary>
        /// Gets or sets the list of student details for download.
        /// </summary>
        public List<SelectListItem> StudentDetails { get; set; }

        /// <summary>
        /// Gets or sets the list of student details selected for download.
        /// </summary>
        public bool[] StudentDetailsSelected { get; set; }

        /// <summary>
        /// Gets or sets the list of parent/guardian details for download.
        /// </summary>
        public List<SelectListItem> GuardianDetails { get; set; }

        /// <summary>
        /// Gets or sets the list of parent/guardian details selected for download.
        /// </summary>
        public bool[] GuardianDetailsSelected { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="DownloadViewModel"/> class.
        /// </summary>
        public void Initialise()
        {
            this.StudentDetails = CommonItemsGenerator.GetStudentDetails()
                                                      .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString(), Selected = true })
                                                      .ToList();

            this.StudentDetailsSelected = new bool[this.StudentDetails.Count];

            this.GuardianDetails = CommonItemsGenerator.GetGuardianDetails()
                                                       .Select(p => new SelectListItem() { Text = p.Key, Value = p.Value.ToString(), Selected = true })
                                                       .ToList();

            this.GuardianDetailsSelected = new bool[this.GuardianDetails.Count];
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        public DownloadViewModel Clone(bool initialise = true)
        {
            var vm = new DownloadViewModel(this, initialise);

            return vm;
        }
    }
}
