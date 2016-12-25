using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IStudentDetailsService : IDisposable
    {
        Task<StudentDetailsViewModel> GetStudentDetailsAsync(Guid formId);

        Task SetStudentDetailsAsync(Guid formId, StudentDetailsViewModel model);
    }
}