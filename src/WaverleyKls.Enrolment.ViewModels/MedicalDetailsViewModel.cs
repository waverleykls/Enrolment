namespace WaverleyKls.Enrolment.ViewModels
{
    public class MedicalDetailsViewModel
    {
        public MedicalDetailsViewModel()
        {
        }

        public MedicalDetailsViewModel(MedicalDetailsViewModel model) : this()
        {
            this.HasAnyMedicalConcern = model.HasAnyMedicalConcern;
            this.MedicalConcernDetails = model.MedicalConcernDetails;
            this.IsTakingMedication = model.IsTakingMedication;
            this.MedicationDetails = model.MedicationDetails;
        }

        public bool HasAnyMedicalConcern { get; set; }

        public string MedicalConcernDetails { get; set; }

        public bool IsTakingMedication { get; set; }

        public string MedicationDetails { get; set; }

        public string Direction { get; set; }
    }
}
