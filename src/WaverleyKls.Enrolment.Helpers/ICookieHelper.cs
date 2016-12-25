using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace WaverleyKls.Enrolment.Helpers
{
    public interface ICookieHelper : IDisposable
    {
        Task ClearFormIdAsync(Controller controller);

        Task<Guid> GetFormIdAsync(Controller controller);
    }
}