﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.Settings;
using WaverleyKls.Enrolment.Settings.Exceptions;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services
{
    /// <summary>
    /// This represents the service entity for email through SendGrid.
    /// </summary>
    public class SendGridMailService : ISendGridMailService
    {
        private readonly SendGridSettings _sgSettings;
        private readonly JsonSerializerSettings _jsSettings;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="SendGridMailService"/> class.
        /// </summary>
        /// <param name="sendGridSettings"><see cref="SendGridSettings"/> instance.</param>
        /// <param name="jsonSerialiserSettings"><see cref="JsonSerializerSettings"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sendGridSettings"/> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="jsonSerialiserSettings"/> is <see langword="null" />.</exception>
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

        /// <summary>
        /// Gets the email template.
        /// </summary>
        /// <param name="templateName">Template name.</param>
        /// <returns>Returns the <see cref="EmailTemplateViewModel"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="templateName"/> is <see langword="null" />.</exception>
        /// <exception cref="AppSettingsException">EndpointUri not found.</exception>
        /// <exception cref="AppSettingsException">Template not found.</exception>
        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is neither 200 (OK) nor 202 (Accepted).</exception>
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

        /// <summary>
        /// Gets the <see cref="EmailViewModel"/> for confirmation to be sent to the applicant.
        /// </summary>
        /// <param name="sd"><see cref="StudentDetailsViewModel"/> object.</param>
        /// <param name="gd"><see cref="GuardianDetailsViewModel"/> object.</param>
        /// <param name="amount">Amount to pay.</param>
        /// <param name="referenceNumber">Reference number.</param>
        /// <param name="template"><see cref="EmailTemplateViewModel"/> object.</param>
        /// <returns>Returns the <see cref="EmailViewModel"/> for confirmation to be sent to the applicant.</returns>
        public EmailViewModel GetConfirmationEmailViewModelForApplicant(StudentDetailsViewModel sd, GuardianDetailsViewModel gd, decimal amount, string referenceNumber, EmailTemplateViewModel template)
        {
            var vm = new EmailViewModel()
                     {
                         Personalizations =
                         {
                             new Personalisation()
                             {
                                 To =
                                 {
                                     new MailAddress()
                                     {
                                         Name = $"{gd.FirstName} {gd.LastName}",
                                         Email = gd.Email
                                     }
                                 }
                             }
                         },
                         Subject = template.Subject.Replace(":name", $"{sd.FirstName} {sd.LastName}"),
                         Content =
                         {
                             new Content()
                             {
                                 Type = "text/plain", // TODO: implement enum
                                 Value =
                                     template.PlainContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                             },
                             new Content()
                             {
                                 Type = "text/html",
                                 Value =
                                     template.HtmlContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                             },
                         }
                     };

            return vm;
        }

        /// <summary>
        /// Gets the <see cref="EmailViewModel"/> for confirmation to be sent to the admin.
        /// </summary>
        /// <param name="sd"><see cref="StudentDetailsViewModel"/> object.</param>
        /// <param name="gd"><see cref="GuardianDetailsViewModel"/> object.</param>
        /// <param name="amount">Amount to pay.</param>
        /// <param name="referenceNumber">Reference number.</param>
        /// <param name="template"><see cref="EmailTemplateViewModel"/> object.</param>
        /// <returns>Returns the <see cref="EmailViewModel"/> for confirmation to be sent to the admin.</returns>
        public EmailViewModel GetConfirmationEmailViewModelForAdmin(StudentDetailsViewModel sd, GuardianDetailsViewModel gd, decimal amount, string referenceNumber, EmailTemplateViewModel template)
        {
            var vm = new EmailViewModel()
                     {
                         Personalizations =
                         {
                             new Personalisation()
                             {
                                 To =
                                 {
                                     new MailAddress()
                                     {
                                         Name = "Waverley KLS Admin",
                                         Email = "waverleykls@outlook.com"
                                     },
                                     new MailAddress()
                                     {
                                         Name = "Waverley KLS Principal",
                                         Email = "mk7189@hotmail.com"
                                     }
                                 }
                             }
                         },
                         Subject = template.Subject.Replace(":name", $"{sd.FirstName} {sd.LastName}"),
                         Content =
                         {
                             new Content()
                             {
                                 Type = "text/plain", // TODO: implement enum
                                 Value =
                                     template.PlainContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                                             .Replace(":guardian", $"{gd.FirstName} {gd.LastName}")
                                             .Replace(":email", gd.Email)
                                             .Replace(":home", gd.HomePhone)
                                             .Replace(":work", gd.WorkPhone)
                                             .Replace(":mobile", gd.MobilePhone)
                             },
                             new Content()
                             {
                                 Type = "text/html",
                                 Value =
                                     template.HtmlContent.Replace(":name", $"{sd.FirstName} {sd.LastName}")
                                             .Replace(":referenceNumber", referenceNumber)
                                             .Replace(":amount", amount.ToString("F2"))
                                             .Replace(":guardian", $"{gd.FirstName} {gd.LastName}")
                                             .Replace(":email", gd.Email)
                                             .Replace(":home", gd.HomePhone)
                                             .Replace(":work", gd.WorkPhone)
                                             .Replace(":mobile", gd.MobilePhone)
                             },
                         }
                     };

            return vm;
        }

        /// <summary>
        /// Sends the email through SendGrid Web API.
        /// </summary>
        /// <param name="model"><see cref="EmailViewModel"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        /// <exception cref="AppSettingsException">EndpointUri not found.</exception>
        /// <exception cref="HttpException"><see cref="HttpStatusCode"/> is neither 200 (OK) nor 202 (Accepted).</exception>
        public async Task SendAsync(EmailViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.From = new MailAddress() { Name = this._sgSettings.From.Name, Email = this._sgSettings.From.Email };

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