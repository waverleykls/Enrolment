using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="EmergencyContactDetailsService"/> class.
    /// </summary>
    public interface IEmergencyContactDetailsService : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="EmergencyContactDetailsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="EmergencyContactDetailsViewModel"/> instance.</returns>
        Task<EmergencyContactDetailsViewModel> GetEmergencyContactDetailsAsync(Guid formId);

        /// <summary>
        /// Merges emergency contact details with parent/guardian details.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="EmergencyContactDetailsViewModel"/> instance.</param>
        /// <returns>Returns the <see cref="EmergencyContactDetailsViewModel"/> instance merged.</returns>
        Task<EmergencyContactDetailsViewModel> MergeGuardianDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model);

        /// <summary>
        /// Saves the <see cref="EmergencyContactDetailsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="EmergencyContactDetailsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> SaveEmergencyContactDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model);
    }
}