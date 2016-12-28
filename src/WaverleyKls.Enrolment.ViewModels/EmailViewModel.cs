using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class EmailViewModel
    {
        public EmailViewModel()
        {
            this.Personalizations = new List<Personalisation>();
            this.Content = new List<Content>();
        }

        public MailAddress From { get; set; }
        public List<Personalisation> Personalizations { get; set; }
        public string Subject { get; set; }
        public List<Content> Content { get; set; }
    }

    public class MailAddress
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class Personalisation
    {
        public Personalisation()
        {
            this.To = new List<MailAddress>();
        }

        public List<MailAddress> To { get; set; }
    }

    public class Content
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}