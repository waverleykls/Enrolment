namespace WaverleyKls.Enrolment.ViewModels
{
    public class ConfirmationViewModel
    {
        public ConfirmationViewModel()
        {
        }

        public StudentDetailsViewModel StudentDetails { get; set; }
        public GuardianDetailsViewModel GuardianDetails { get; set; }
        public EmergencyContactDetailsViewModel EmergencyContactDetails { get; set; }
        public MedicalDetailsViewModel MedicalDetails { get; set; }
        public GuardianConsentsViewModel GuardianConsents { get; set; }

        public string Direction { get; set; }
    }
}