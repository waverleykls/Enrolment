using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

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

        public async Task<string> GetFormIdAsync(Controller controller)
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

        private string GetFormId(Controller controller)
        {
            string formId;
            if (controller.Request.Cookies.TryGetValue(FormId, out formId))
            {
                return formId;
            }

            formId = Guid.NewGuid().ToString();

            controller.Response.Cookies.Append(FormId, formId);

            return formId;
        }
    }
}
