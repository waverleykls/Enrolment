using System.ComponentModel.DataAnnotations;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for parent/guardian consents page.
    /// </summary>
    public class GuardianConsentsViewModel : IInitialisable, ICloneable<GuardianConsentsViewModel>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GuardianConsentsViewModel"/> class.
        /// </summary>
        public GuardianConsentsViewModel()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="GuardianConsentsViewModel"/> class.
        /// </summary>
        /// <param name="model"><see cref="GuardianConsentsViewModel"/> instance.</param>
        /// <param name="initialise">Value that indicates whether to initialise or not.</param>
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

        /// <summary>
        /// Gets or sets the value that indicates whether to agree ToC or not.
        /// </summary>
        [Required]
        public bool AgreeToc { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether to agree with taking student's photo or not.
        /// </summary>
        [Required]
        public bool AgreePhoto { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether to agree with receiving SMS or not.
        /// </summary>
        [Required]
        public bool AgreeSms { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether to agree with receiving KakaoTalk messages or not.
        /// </summary>
        [Required]
        public bool AgreeKakaoTalk { get; set; }

        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the direction for page navigation.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Initialises with pre-defined values.
        /// </summary>
        public void Initialise()
        {
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <param name="initialise">Value that indicates whether to initialise the cloned object or not.</param>
        /// <returns>Returns the instance cloned.</returns>
        public GuardianConsentsViewModel Clone(bool initialise = true)
        {
            var vm = new GuardianConsentsViewModel(this, initialise);

            return vm;
        }
    }
}