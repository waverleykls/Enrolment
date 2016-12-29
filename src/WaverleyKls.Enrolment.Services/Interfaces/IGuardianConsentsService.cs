using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="GuardianConsentsService"/> class.
    /// </summary>
    public interface IGuardianConsentsService : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="GuardianConsentsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="GuardianConsentsViewModel"/> instance.</returns>
        Task<GuardianConsentsViewModel> GetGuardianConsentsAsync(Guid formId);

        /// <summary>
        /// Saves the <see cref="GuardianConsentsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="GuardianConsentsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> SaveGuardianConsentsAsync(Guid formId, GuardianConsentsViewModel model);
    }
}