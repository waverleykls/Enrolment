using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.Helpers.Interfaces;

namespace WaverleyKls.Enrolment.Helpers
{
    /// <summary>
    /// This represents the helper entity for the cookie.
    /// </summary>
    public class CookieHelper : ICookieHelper
    {
        private const string FormId = "formId";

        private bool _disposed;

        /// <summary>
        /// Clears the enrolment form Id from the cookie.
        /// </summary>
        /// <param name="controller"><see cref="Controller"/> instance.</param>
        public async Task ClearFormIdAsync(Controller controller)
        {
            await Task.Factory.StartNew(() => ClearFormId(controller)).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the enrolment form Id from the cookie.
        /// </summary>
        /// <param name="controller"><see cref="Controller"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
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
