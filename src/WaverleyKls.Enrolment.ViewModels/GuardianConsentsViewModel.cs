using System.ComponentModel.DataAnnotations;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class GuardianConsentsViewModel : IInitialisable, ICloneable<GuardianConsentsViewModel>
    {
        public GuardianConsentsViewModel()
        {
            this.Initialise();
        }

        private GuardianConsentsViewModel(GuardianConsentsViewModel model, bool initialise = true)
        {
            if (model == null)
            {
                return;
            }

            if (initialise)
            {
                this.Initialise();
            }

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
        public void Initialise()
        {
        }

        public GuardianConsentsViewModel Clone(bool initialise = true)
        {
            var vm = new GuardianConsentsViewModel(this, initialise);

            return vm;
        }
    }
}