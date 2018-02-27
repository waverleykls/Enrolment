using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using WaverleyKls.Enrolment.EntityModels;
using WaverleyKls.Enrolment.Services.Interfaces;
using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services
{
    /// <summary>
    /// This represents the service entity for download.
    /// </summary>
    public class DownloadService : IDownloadService
    {
        private readonly IWklsDbContext _context;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadService"/> class.
        /// </summary>
        /// <param name="context"><see cref="IWklsDbContext"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="context"/> is <see langword="null" />.</exception>
        public DownloadService(IWklsDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        /// <summary>
        /// Gets the list of downloadable items from enrolment details.
        /// </summary>
        /// <returns>Returns the list of downloadable items from enrolment details.</returns>
        public async Task<DownloadViewModel> GetDownloadableItemsAsync()
        {
            var vm = await Task.Factory.StartNew(this.GetDownloadableItems).ConfigureAwait(false);

            return vm;
        }

        /// <summary>
        /// Process the download.
        /// </summary>
        /// <param name="model"><see cref="DownloadViewModel"/> instance.</param>
        /// <returns>Returns the downloadable stream processed.</returns>
        public async Task<List<DownloadableViewModel>> ProcessDownloadAsync(DownloadViewModel model)
        {
            var dms = await this._context.Payments
                                .Include(p => p.EnrolmentForm)
                                .Where(p => this.IsDownloadable(model, p))
                                .Select(p => this.GetDownloadable(model, p))
                                .OrderByDescending(p => p.DateEnrolled)
                                .ThenBy(p => p.StudentName)
                                .ThenBy(p => p.GuardianName)
                                .ToListAsync().ConfigureAwait(false);

            return dms;
            //var sb = new StringBuilder();

            //var header = "\"Name\"\t\"School\"\t\"Year\"\t\"Gender\"\t\"Date of Birth\"\t\"Address\"\t\"Parent/Guardian Name\"\t\"Parent/Guardian Contact\"\t\"Parent/Guardian Email\"";
            //sb.AppendLine(header);

            //foreach (var dm in dms)
            //{
            //    var body = $"\"{dm.StudentName}\"\t\"{dm.StudentSchool}\"\t\"{dm.StudentYear}\"\t\"{dm.StudentGender}\"\t\"{dm.StudentDateOfBirth}\"\t\"{dm.StudentAddress}\"\t\"{dm.GuardianName}\"\t\"{dm.GuardianContact}\"\t\"{dm.GuardianEmail}\"";
            //    sb.AppendLine(body);
            //}

            //return sb.ToString();
        }

        private DownloadableViewModel GetDownloadable(DownloadViewModel model, Payment payment)
        {
            var sd = JsonConvert.DeserializeObject<StudentDetailsViewModel>(payment.EnrolmentForm.StudentDetails);
            var gd = JsonConvert.DeserializeObject<GuardianDetailsViewModel>(payment.EnrolmentForm.GuardianDetails);

            var tzi = TimeZoneInfo.GetSystemTimeZones().SingleOrDefault(p => p.Id == "AUS Eastern Standard Time");
            var offset = tzi.GetUtcOffset(payment.EnrolmentForm.DateCreated);

            var dm = new DownloadableViewModel(model, sd, gd, payment.DatePaid > DateTimeOffset.MinValue, payment.EnrolmentForm.DateCreated.ToOffset(offset));

            return dm;
        }

        private bool IsDownloadable(DownloadViewModel model, Payment payment)
        {
            if (model.IsPaidOnly && payment.DatePaid == DateTimeOffset.MinValue)
            {
                return false;
            }

            var sd = JsonConvert.DeserializeObject<StudentDetailsViewModel>(payment.EnrolmentForm.StudentDetails);
            if (!sd.IsDomestic)
            {
                return false;
            }

            if (sd.YearLevel == "K")
            {
                return false;
            }

            return true;
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

        private DownloadViewModel GetDownloadableItems()
        {
            return new DownloadViewModel();
        }
    }
}
