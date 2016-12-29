using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="MedicalDetailsService"/> class.
    /// </summary>
    public interface IMedicalDetailsService : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="MedicalDetailsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="MedicalDetailsViewModel"/> instance.</returns>
        Task<MedicalDetailsViewModel> GetMedicalDetailsAsync(Guid formId);

        /// <summary>
        /// Saves the <see cref="MedicalDetailsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="MedicalDetailsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> SaveMedicalDetailsAsync(Guid formId, MedicalDetailsViewModel model);
    }
}