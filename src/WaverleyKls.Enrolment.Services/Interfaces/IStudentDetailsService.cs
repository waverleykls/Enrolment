using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="StudentDetailsService"/> class.
    /// </summary>
    public interface IStudentDetailsService : IDisposable
    {
        /// <summary>
        /// Gets the <see cref="StudentDetailsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="StudentDetailsViewModel"/> instance.</returns>
        Task<StudentDetailsViewModel> GetStudentDetailsAsync(Guid formId);

        /// <summary>
        /// Saves the <see cref="StudentDetailsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="StudentDetailsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        Task<Guid> SaveStudentDetailsAsync(Guid formId, StudentDetailsViewModel model);
    }
}