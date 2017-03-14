using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using WaverleyKls.Enrolment.ViewModels;

namespace WaverleyKls.Enrolment.Services.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <see cref="DownloadService"/> class.
    /// </summary>
    public interface IDownloadService : IDisposable
    {
        /// <summary>
        /// Gets the list of downloadable items from enrolment details.
        /// </summary>
        /// <returns>Returns the list of downloadable items from enrolment details.</returns>
        Task<DownloadViewModel> GetDownloadableItemsAsync();

        /// <summary>
        /// Process the download.
        /// </summary>
        /// <param name="model"><see cref="DownloadViewModel"/> instance.</param>
        /// <returns>Returns the downloadable stream processed.</returns>
        Task<List<DownloadableViewModel>> ProcessDownloadAsync(DownloadViewModel model);
    }
}