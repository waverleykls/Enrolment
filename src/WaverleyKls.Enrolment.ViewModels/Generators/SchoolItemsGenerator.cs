﻿using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels.Generators
{
    /// <summary>
    /// This represents the generator entity used for school in view models.
    /// </summary>
    public static class SchoolItemsGenerator
    {
        /// <summary>
        /// Gets the list of school years.
        /// </summary>
        /// <returns>Returns the list of school years.</returns>
        public static IEnumerable<KeyValuePair<string, string>> GetYearLevels()
        {
            var levels = new[]
                         {
                             new KeyValuePair<string, string>("Kindergarten", "K"),
                             new KeyValuePair<string, string>("Prep", "P"),
                             new KeyValuePair<string, string>("Year 1", "1"),
                             new KeyValuePair<string, string>("Year 2", "2"),
                             new KeyValuePair<string, string>("Year 3", "3"),
                             new KeyValuePair<string, string>("Year 4", "4"),
                             new KeyValuePair<string, string>("Year 5", "5"),
                             new KeyValuePair<string, string>("Year 6", "6"),
                             new KeyValuePair<string, string>("Year 7", "7"),
                             new KeyValuePair<string, string>("Year 8", "8"),
                             new KeyValuePair<string, string>("Year 9", "9"),
                             new KeyValuePair<string, string>("Year 10", "10"),
                             new KeyValuePair<string, string>("Year 11", "11"),
                             new KeyValuePair<string, string>("Year 12", "12"),
                         };
            return levels;
        }

        /// <summary>
        /// Gets the list of school fee schemes.
        /// </summary>
        /// <returns>Returns the list of school fee schemes.</returns>
        public static IEnumerable<KeyValuePair<string, bool>> GetFeeSchemes()
        {
            var schemes = new[]
                         {
                             new KeyValuePair<string, bool>("Australian Citizen or Permanent Resident", true),
                             new KeyValuePair<string, bool>("Full-fee Paying or International Student", false),
                         };
            return schemes;
        }
    }
}