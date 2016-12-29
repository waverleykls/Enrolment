namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for confirmation page.
    /// </summary>
    public class ConfirmationViewModel
    {
        /// <summary>
        /// Gets or sets the <see cref="StudentDetailsViewModel"/> instance.
        /// </summary>
        public StudentDetailsViewModel StudentDetails { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="GuardianDetailsViewModel"/> instance.
        /// </summary>
        public GuardianDetailsViewModel GuardianDetails { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="EmergencyContactDetailsViewModel"/> instance.
        /// </summary>
        public EmergencyContactDetailsViewModel EmergencyContactDetails { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MedicalDetailsViewModel"/> instance.
        /// </summary>
        public MedicalDetailsViewModel MedicalDetails { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="GuardianConsentsViewModel"/> instance.
        /// </summary>
        public GuardianConsentsViewModel GuardianConsents { get; set; }

        /// <summary>
        /// Gets or sets the direction for page navigation.
        /// </summary>
        public string Direction { get; set; }
    }
}