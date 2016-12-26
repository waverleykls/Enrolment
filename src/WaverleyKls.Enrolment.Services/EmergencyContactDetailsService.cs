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
    public class EmergencyContactDetailsService : IEmergencyContactDetailsService
    {
        private readonly IWklsDbContext _context;

        private bool _disposed;

        public EmergencyContactDetailsService(IWklsDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        public async Task<EmergencyContactDetailsViewModel> GetEmergencyContactDetailsAsync(Guid formId)
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

            if (form.EmergencyContactDetails.IsNullOrWhiteSpace())
            {
                return null;
            }

            var model = JsonConvert.DeserializeObject<EmergencyContactDetailsViewModel>(form.EmergencyContactDetails);

            return model;
        }

        public async Task<EmergencyContactDetailsViewModel> MergeGuardianDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(formId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!model.IsSameAsGuardianDetails)
            {
                return model;
            }

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form.GuardianDetails.IsNullOrWhiteSpace())
            {
                model.FirstName = null;
                model.MiddleNames = null;
                model.LastName = null;
                model.RelationshipToStudent = null;
                model.HomePhone = null;
                model.WorkPhone = null;
                model.MobilePhone = null;
                model.Email = null;

                return model;
            }

            var gd = JsonConvert.DeserializeObject<GuardianDetailsViewModel>(form.GuardianDetails);

            model.FirstName = gd.FirstName;
            model.MiddleNames = gd.MiddleNames;
            model.LastName = gd.LastName;
            model.RelationshipToStudent = gd.RelationshipToStudent;
            model.HomePhone = gd.HomePhone;
            model.WorkPhone = gd.WorkPhone;
            model.MobilePhone = gd.MobilePhone;
            model.Email = gd.Email;

            return model;
        }

        public async Task<Guid> SaveEmergencyContactDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(formId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var form = await this.AddOrUpdateEmergencyContactDetailsAsync(formId, model).ConfigureAwait(false);

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

        private async Task<EnrolmentForm> AddOrUpdateEmergencyContactDetailsAsync(Guid formId, EmergencyContactDetailsViewModel model)
        {
            var now = DateTimeOffset.UtcNow;

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form == null)
            {
                form = new EnrolmentForm() { FormId = formId, DateCreated = now };
            }

            form.EmergencyContactDetails = JsonConvert.SerializeObject(model);
            form.DateUpdated = now;

            this._context.AddOrUpdate(form);

            return form;
        }
    }
}