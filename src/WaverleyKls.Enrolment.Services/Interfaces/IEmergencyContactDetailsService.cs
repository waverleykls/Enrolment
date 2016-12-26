using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IEmergencyContactDetailsService : IDisposable
    {
        Task<EmergencyContactDetailsViewModel> GetEmergencyContactDetailsAsync(Guid formId);
        Task<EmergencyContactDetailsViewModel> MergeGuardianDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model);

        Task<Guid> SaveEmergencyContactDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model);
    }
}