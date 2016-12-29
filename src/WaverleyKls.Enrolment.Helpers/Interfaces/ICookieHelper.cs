using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace WaverleyKls.Enrolment.Helpers.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="CookieHelper"/> class.
    /// </summary>
    public interface ICookieHelper : IDisposable
    {
        /// <summary>
        /// Clears the enrolment form Id from the cookie.
        /// </summary>
        /// <param name="controller"><see cref="Controller"/> instance.</param>
        Task ClearFormIdAsync(Controller controller);

        /// <summary>
        /// Gets the enrolment form Id from the cookie.
        /// </summary>
        /// <param name="controller"><see cref="Controller"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> GetFormIdAsync(Controller controller);
    }
}