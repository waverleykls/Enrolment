﻿using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Extensions;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services
{
    /// <summary>
    /// This represents the service entity for the medical details in the enrolment form.
    /// </summary>
    public class MedicalDetailsService : IMedicalDetailsService
    {
        private readonly IWklsDbContext _context;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="MedicalDetailsService"/> class.
        /// </summary>
        /// <param name="context"><see cref="IWklsDbContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public MedicalDetailsService(IWklsDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the <see cref="MedicalDetailsViewModel"/> instance.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <returns>Returns the <see cref="MedicalDetailsViewModel"/> instance.</returns>
        /// <exception cref="ArgumentException">Invalid enrolment form Id.</exception>
        public async Task<MedicalDetailsViewModel> GetMedicalDetailsAsync(Guid formId)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid enrolment form Id", nameof(formId));
            }

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form == null)
            {
                return null;
            }

            if (form.MedicalDetails.IsNullOrWhiteSpace())
            {
                return null;
            }

            var model = JsonConvert.DeserializeObject<MedicalDetailsViewModel>(form.MedicalDetails);

            return model;
        }

        /// <summary>
        /// Saves the <see cref="MedicalDetailsViewModel"/> into the database.
        /// </summary>
        /// <param name="formId">Enrolment form Id.</param>
        /// <param name="model"><see cref="MedicalDetailsViewModel"/> instance.</param>
        /// <returns>Returns the enrolment form Id.</returns>
        /// <exception cref="ArgumentException">Invalid enrolment form Id.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null" />.</exception>
        public async Task<Guid> SaveMedicalDetailsAsync(Guid formId, MedicalDetailsViewModel model)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid enrolment form Id", nameof(formId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var form = await this.AddOrUpdateMedicalDetailsAsync(formId, model).ConfigureAwait(false);

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

        private async Task<EnrolmentForm> AddOrUpdateMedicalDetailsAsync(Guid formId, MedicalDetailsViewModel model)
        {
            var now = DateTimeOffset.UtcNow;

            var form = await this._context.EnrolmentForms.SingleOrDefaultAsync(p => p.FormId == formId).ConfigureAwait(false);
            if (form == null)
            {
                form = new EnrolmentForm() { FormId = formId, DateCreated = now };
            }

            form.MedicalDetails = JsonConvert.SerializeObject(model);
            form.DateUpdated = now;

            this._context.AddOrUpdate(form);

            return form;
        }
    }
}