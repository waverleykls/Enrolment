using System;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IGuardianConsentsService : IDisposable
    {
        Task<GuardianConsentsViewModel> GetGuardianConsentsAsync(Guid formId);

        Task<Guid> SaveGuardianConsentsAsync(Guid formId, GuardianConsentsViewModel model);
    }
}