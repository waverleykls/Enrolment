using System.ComponentModel.DataAnnotations;

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

        [Required]
        public bool AgreeToc { get; set; }

        [Required]
        public bool AgreePhoto { get; set; }

        [Required]
        public bool AgreeSms { get; set; }

        [Required]
        public bool AgreeKakaoTalk { get; set; }
        public string Comments { get; set; }
        public string Direction { get; set; }
    }
}