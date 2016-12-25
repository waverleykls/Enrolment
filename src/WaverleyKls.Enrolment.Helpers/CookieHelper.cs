using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WaverleyKls.Enrolment.Extensions;

namespace WaverleyKls.Enrolment.Helpers
{
    public class CookieHelper : ICookieHelper
    {
        private const string FormId = "formId";

        private bool _disposed;

        public async Task ClearFormIdAsync(Controller controller)
        {
            await Task.Factory.StartNew(() => ClearFormId(controller)).ConfigureAwait(false);
        }

        public async Task<Guid> GetFormIdAsync(Controller controller)
        {
            var formId = await Task.Factory.StartNew(() => this.GetFormId(controller)).ConfigureAwait(false);

            return formId;
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

        private void ClearFormId(Controller controller)
        {
            controller.Response.Cookies.Delete(FormId);
        }

        private Guid GetFormId(Controller controller)
        {
            Guid formId;
            string base64EncodedFormId;
            if (controller.Request.Cookies.TryGetValue(FormId, out base64EncodedFormId))
            {
                formId = base64EncodedFormId.ToGuid();
                return formId;
            }

            formId = Guid.NewGuid();

            controller.Response.Cookies.Append(FormId, formId.ToBase64String());

            return formId;
        }
    }
}
