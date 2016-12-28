using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.Settings;
using WaverleyKls.Enrolment.Settings.Exceptions;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services
{
    public class SendGridMailService : ISendGridMailService
    {
        private readonly SendGridSettings _sgSettings;
        private readonly JsonSerializerSettings _jsSettings;

        private bool _disposed;

        public SendGridMailService(SendGridSettings sendGridSettings, JsonSerializerSettings jsonSerialiserSettings)
        {
            if (sendGridSettings == null)
            {
                throw new ArgumentNullException(nameof(sendGridSettings));
            }

            this._sgSettings = sendGridSettings;

            if (jsonSerialiserSettings == null)
            {
                throw new ArgumentNullException(nameof(jsonSerialiserSettings));
            }

            this._jsSettings = jsonSerialiserSettings;
        }

        public async Task<EmailTemplateViewModel> GetEmailTemplateAsync(string templateName)
        {
            if (templateName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(templateName));
            }

            using (var client = new HttpClient() { BaseAddress = new Uri(this._sgSettings.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._sgSettings.ApiKey);

                var ep = this._sgSettings.EndpointUris.SingleOrDefault(p => p.Name.Equals("Template", StringComparison.CurrentCultureIgnoreCase));
                if (ep == null)
                {
                    throw new AppSettingsException("EndpointUri not found");
                }

                var tp = this._sgSettings.Templates.SingleOrDefault(p => p.Name.Equals(templateName, StringComparison.CurrentCultureIgnoreCase));
                if (tp == null)
                {
                    throw new AppSettingsException("Template not found");
                }

                var response = await client.GetAsync(string.Format($"{this._sgSettings.Version}/{ep.Uri}", tp.TemplateId, tp.VersionId)).ConfigureAwait(false);
                var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!IsResponseSuccessful(response.StatusCode))
                {
                    throw new HttpException((int)response.StatusCode, msg);
                }

                var converted = JsonConvert.DeserializeObject<EmailTemplateViewModel>(msg);
                return converted;
            }
        }

        public async Task SendAsync(EmailViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.From = new MailAddress() { Name = this._sgSettings.From.Name, Email = this._sgSettings.From.Email };
            //model = new EmailViewModel()
            //        {
            //            From = new MailAddress() { Name = "Waverley Korean Language School", Email = "waverleykls@outlook.com" },
            //            Personalizations = { new Personalisation() { To = { new MailAddress() { Name = "Justin Yoo", Email = "justin.yoojh@gmail.com" } } } },
            //            Subject = "SendGrid TEST TEST",
            //            Content = { new Content() { Type = "text/html", Value = "<strong>Hello World</strong>" } }
            //        };

            var serialised = JsonConvert.SerializeObject(model, this._jsSettings);

            using (var client = new HttpClient() { BaseAddress = new Uri(this._sgSettings.BaseUri) })
            using (var content = new StringContent(serialised))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._sgSettings.ApiKey);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var ep = this._sgSettings.EndpointUris.SingleOrDefault(p => p.Name.Equals("SendMail", StringComparison.CurrentCultureIgnoreCase));
                if (ep == null)
                {
                    throw new AppSettingsException("EndpointUri not found");
                }

                var response = await client.PostAsync($"{this._sgSettings.Version}/{ep.Uri}", content).ConfigureAwait(false);

                if (!IsResponseSuccessful(response.StatusCode))
                {
                    var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new HttpException((int) response.StatusCode, msg);
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private static bool IsResponseSuccessful(HttpStatusCode sc)
        {
            var successful = sc == HttpStatusCode.OK || sc == HttpStatusCode.Accepted;
            return successful;
        }
    }
}