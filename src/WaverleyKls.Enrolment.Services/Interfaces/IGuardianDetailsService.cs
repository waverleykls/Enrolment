using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="GuardianDetailsService"/> class.
    /// </summary>
    public interface IGuardianDetailsService : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="GuardianDetailsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="GuardianDetailsViewModel"/> instance.</returns>
        Task<GuardianDetailsViewModel> GetGuardianDetailsAsync(Guid formId);

        /// <summary>
        /// Saves the <see cref="GuardianDetailsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="GuardianDetailsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> SaveGuardianDetailsAsync(Guid formId, GuardianDetailsViewModel model);
    }
}