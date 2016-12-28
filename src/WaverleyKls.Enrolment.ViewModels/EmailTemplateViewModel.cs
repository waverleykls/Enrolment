using System;

using Newtonsoft.Json;

namespace WaverleyKls.Enrolment.ViewModels
{
    public class EmailTemplateViewModel
    {
        [JsonProperty("template_id")]
        public Guid TemplateId { get; set; }

        [JsonProperty("id")]
        public Guid VersionId { get; set; }

        public int Active { get; set; }

        public string Name { get; set; }

        [JsonProperty("html_content")]
        public string HtmlContent { get; set; }

        [JsonProperty("plain_content")]
        public string PlainContent { get; set; }

        public string Subject { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}