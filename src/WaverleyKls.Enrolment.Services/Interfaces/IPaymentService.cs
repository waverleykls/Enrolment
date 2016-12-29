using System;
using System.Threading.Tasks;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    public interface IPaymentService : IDisposable
    {
        Task<string> GetReferenceNumberByFormIdAsync(Guid formId);
        Task<decimal> GetAmountAsync(bool isDomestic, string yearLevel, DateTimeOffset now);
        Task<string> SavePaymentAsync(Guid formId, decimal amount);
    }
}