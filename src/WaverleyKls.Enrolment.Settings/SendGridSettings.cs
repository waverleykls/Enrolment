using System;
using System.Collections.Generic;

namespace WaverleyKls.Enrolment.Settings
{
    /// <summary>
    /// This represents the settings entity for SendGrid.
    /// </summary>
    public class SendGridSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SendGridSettings"/> class.
        /// </summary>
        public SendGridSettings()
        {
            this.EndpointUris = new List<EndpointUri>();
            this.Templates = new List<TemplateSettings>();
        }

        /// <summary>
        /// Getsor sets the <see cref="SendGridSettings"/>.
        /// </summary>
        public virtual SenderSettings From { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public virtual string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the Base URI of the SendGrid Web API.
        /// </summary>
        public virtual string BaseUri { get; set; }

        /// <summary>
        /// Gets or sets the Web API version.
        /// </summary>
        public virtual string Version { get; set; } // TODO: implement enum

        /// <summary>
        /// Gets or sets the list of endpoint URIs.
        /// </summary>
        public virtual List<EndpointUri> EndpointUris { get; set; }

        /// <summary>
        /// Gets or sets the list of email template settings.
        /// </summary>
        public virtual List<TemplateSettings> Templates { get; set; }
    }

    /// <summary>
    /// This represents the settings entity for email sender.
    /// </summary>
    public class SenderSettings
    {
        /// <summary>
        /// Gets or sets the sender's name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the sender's email.
        /// </summary>
        public virtual string Email { get; set; }
    }

    /// <summary>
    /// This represents the settings entity for endpoint URI.
    /// </summary>
    public class EndpointUri
    {
        /// <summary>
        /// Gets or sets the endpoint name.
        /// </summary>
        public virtual string Name { get; set; } // TODO: implement enum

        /// <summary>
        /// Gets or sets the endpoint URI.
        /// </summary>
        public virtual string Uri { get; set; }
    }

    /// <summary>
    /// This represents the settings entity for email teamplate.
    /// </summary>
    public class TemplateSettings
    {
        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        public virtual string Name { get; set; } // TODO: implement enum

        /// <summary>
        /// Gets or sets the template Id.
        /// </summary>
        public virtual Guid TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the version Id.
        /// </summary>
        public virtual Guid VersionId { get; set; }
    }
}
