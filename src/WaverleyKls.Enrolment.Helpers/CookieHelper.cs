using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WaverleyKls.Enrolment.Helpers
{
    public class CookieHelper
    {
        private const string FormId = "formId";

        private readonly CookieOptions _options;

        public CookieHelper(CookieOptions options = null)
        {
            this._options = options ?? new CookieOptions() { HttpOnly = true, Secure = true };
        }

        public string GetFormId(Controller controller, CookieOptions options = null)
        {
            string formId;
            if (controller.Request.Cookies.TryGetValue(FormId, out formId))
            {
                return formId;
            }

            formId = Guid.NewGuid().ToString();
            controller.Response.Cookies.Append(FormId, formId, options ?? this._options);

            return formId;
        }
    }
}
