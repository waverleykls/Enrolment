using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IMedicalDetailsService : IDisposable
    {
        Task<MedicalDetailsViewModel> GetMedicalDetailsAsync(Guid formId);

        Task<Guid> SaveMedicalDetailsAsync(Guid formId, MedicalDetailsViewModel model);
    }
}