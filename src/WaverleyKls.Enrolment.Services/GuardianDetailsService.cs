using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services
{
    public class GuardianDetailsService : IGuardianDetailsService
    {
        private readonly IWklsDbContext _context;

        private bool _disposed;

        public GuardianDetailsService(IWklsDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        public async Task<GuardianDetailsViewModel> GetGuardianDetailsAsync(Guid formId)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(formId));
            }

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form == null)
            {
                return null;
            }

            if (form.GuardianDetails.IsNullOrWhiteSpace())
            {
                return null;
            }

            var model = JsonConvert.DeserializeObject<GuardianDetailsViewModel>(form.GuardianDetails);

            return model;
        }

        public async Task<Guid> SaveGuardianDetailsAsync(Guid formId, GuardianDetailsViewModel model)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(formId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var form = await this.AddOrUpdateGuardianDetailsAsync(formId, model).ConfigureAwait(false);

            var transaction = await this._context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                await this._context.SaveChangesAsync().ConfigureAwait(false);
                transaction.Commit();

                return form.FormId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private async Task<EnrolmentForm> AddOrUpdateGuardianDetailsAsync(Guid formId, GuardianDetailsViewModel model)
        {
            var now = DateTimeOffset.UtcNow;

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form == null)
            {
                form = new EnrolmentForm() { FormId = formId, DateCreated = now };
            }

            form.GuardianDetails = JsonConvert.SerializeObject(model);
            form.DateUpdated = now;

            this._context.AddOrUpdate(form);

            return form;
        }
    }
}