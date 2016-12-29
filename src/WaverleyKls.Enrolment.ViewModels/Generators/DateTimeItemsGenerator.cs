using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WaverleyKls.Enrolment.ViewModels.Generators
{
    /// <summary>
    /// This represents the generator entity used for date/time in view models.
    /// </summary>
    public static class DateTimeItemsGenerator
    {
        /// <summary>
        /// Gets the list of dates of a month.
        /// </summary>
        /// <returns>Returns the list of dates of a month.</returns>
        public static IEnumerable<KeyValuePair<string, int>> GetDates()
        {
            var dates = Enumerable.Range(1, 31)
                                  .Select(p => new KeyValuePair<string, int>(p.ToString(), p));
            return dates;
        }

        /// <summary>
        /// Gets the list of months of a year.
        /// </summary>
        /// <returns>Gets the list of months of a year.</returns>
        public static IEnumerable<KeyValuePair<string, int>> GetMonths()
        {
            var months = DateTimeFormatInfo.InvariantInfo.AbbreviatedMonthNames
                                           .Where(p => !string.IsNullOrWhiteSpace(p))
                                           .Select((p, i) => new KeyValuePair<string, int>(p, i + 1));
            return months;
        }

        /// <summary>
        /// Gets the list of years.
        /// </summary>
        /// <returns>Returns the list of years.</returns>
        public static IEnumerable<KeyValuePair<string, int>> GetYears()
        {
            var years = Enumerable.Range(DateTimeOffset.UtcNow.Year - 19, 20)
                                  .OrderByDescending(p => p)
                                  .Select(p => new KeyValuePair<string, int>(p.ToString(), p));
            return years;
        }
    }
}