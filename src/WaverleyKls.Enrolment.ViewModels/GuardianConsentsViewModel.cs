namespace WaverleyKls.Enrolment.ViewModels
{
    public class GuardianConsentsViewModel
    {
        public GuardianConsentsViewModel()
        {
        }

        public GuardianConsentsViewModel(GuardianConsentsViewModel model) : this()
        {
            this.AgreeToc = model.AgreeToc;
            this.AgreePhoto = model.AgreePhoto;
            this.AgreeSms = model.AgreeSms;
            this.AgreeKakaoTalk = model.AgreeKakaoTalk;
            this.Comments = model.Comments;
        }

        public bool AgreeToc { get; set; }
        public bool AgreePhoto { get; set; }
        public bool AgreeSms { get; set; }
        public bool AgreeKakaoTalk { get; set; }
        public string Comments { get; set; }
        public string Direction { get; set; }
    }
}