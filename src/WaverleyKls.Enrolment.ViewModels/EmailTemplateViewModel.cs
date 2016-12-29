using System;

using Newtonsoft.Json;

namespace WaverleyKls.Enrolment.ViewModels
{
    /// <summary>
    /// This represents the view model entity for email template.
    /// </summary>
    public class EmailTemplateViewModel
    {
        /// <summary>
        /// Gets or sets the template Id.
        /// </summary>
        [JsonProperty("template_id")]
        public Guid TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the version Id.
        /// </summary>
        [JsonProperty("id")]
        public Guid VersionId { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether the template is active or not.
        /// </summary>
        public int Active { get; set; }

        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email content in HTML format.
        /// </summary>
        [JsonProperty("html_content")]
        public string HtmlContent { get; set; }

        /// <summary>
        /// Gets or sets the email content in plain text format.
        /// </summary>
        [JsonProperty("plain_content")]
        public string PlainContent { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the date when the template was updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}