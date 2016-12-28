using System;
using System.Collections.Generic;

namespace WaverleyKls.Enrolment.Settings
{
    public class SendGridSettings
    {
        public SendGridSettings()
        {
            this.EndpointUris = new List<EndpointUri>();
            this.Templates = new List<TemplateSettings>();
        }

        public virtual SenderSettings From { get; set; }
        public virtual string ApiKey { get; set; }
        public virtual string BaseUri { get; set; }
        public virtual string Version { get; set; } // TODO: implement enum
        public virtual List<EndpointUri> EndpointUris { get; set; }
        public virtual List<TemplateSettings> Templates { get; set; }
    }

    public class SenderSettings
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }

    public class EndpointUri
    {
        public virtual string Name { get; set; } // TODO: implement enum
        public virtual string Uri { get; set; }
    }

    public class TemplateSettings
    {
        public virtual string Name { get; set; } // TODO: implement enum
        public virtual Guid TemplateId { get; set; }
        public virtual Guid VersionId { get; set; }
    }
}
