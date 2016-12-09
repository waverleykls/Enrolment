using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WaverleyKls.Enrolment.ViewModels
{
    public static class DateTimeItemsGenerator
    {
        public static IEnumerable<KeyValuePair<string, int>> GetDates()
        {
            var dates = Enumerable.Range(1, 31)
                                  .Select(p => new KeyValuePair<string, int>(p.ToString(), p));
            return dates;
        }

        public static IEnumerable<KeyValuePair<string, int>> GetMonths()
        {
            var months = DateTimeFormatInfo.InvariantInfo.MonthNames
                                           .Where(p => !string.IsNullOrWhiteSpace(p))
                                           .Select((p, i) => new KeyValuePair<string, int>(p, i + 1));
            return months;
        }

        public static IEnumerable<KeyValuePair<string, int>> GetYears()
        {
            var years = Enumerable.Range(DateTimeOffset.UtcNow.Year - 19, 20)
                                  .OrderByDescending(p => p)
                                  .Select(p => new KeyValuePair<string, int>(p.ToString(), p));
            return years;
        }
    }
}
