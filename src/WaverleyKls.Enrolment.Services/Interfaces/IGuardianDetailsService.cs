using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IGuardianDetailsService : IDisposable
    {
        Task<GuardianDetailsViewModel> GetGuardianDetailsAsync(Guid formId);

        Task<Guid> SaveGuardianDetailsAsync(Guid formId, GuardianDetailsViewModel model);
    }
}