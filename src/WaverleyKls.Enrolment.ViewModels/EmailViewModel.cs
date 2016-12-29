using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for email.
    /// </summary>
    public class EmailViewModel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EmailViewModel"/> class.
        /// </summary>
        public EmailViewModel()
        {
            this.Personalizations = new List<Personalisation>();
            this.Content = new List<Content>();
        }

        /// <summary>
        /// Gets or sets the mail address as a sender.
        /// </summary>
        public MailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the list of personalisations for recipients.
        /// </summary>
        public List<Personalisation> Personalizations { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the list of content in different format.
        /// </summary>
        public List<Content> Content { get; set; }
    }

    /// <summary>
    /// This represents the entity for mail address.
    /// </summary>
    public class MailAddress
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// This represents the entity for personalisation of recipient.
    /// </summary>
    public class Personalisation
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Personalisation"/> class.
        /// </summary>
        public Personalisation()
        {
            this.To = new List<MailAddress>();
        }

        /// <summary>
        /// Gets or sets the list of mail addresses as recipients.
        /// </summary>
        public List<MailAddress> To { get; set; }
    }

    /// <summary>
    /// This represents the entity for email content.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Gets or sets the MIME type of the content.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        public string Value { get; set; }
    }
}