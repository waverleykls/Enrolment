using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels.Generators
{
    public static class CommonItemsGenerator
    {
        public static IEnumerable<KeyValuePair<string, bool>> GetAnswers()
        {
            var answers = new[]
                          {
                              new KeyValuePair<string, bool>("Yes", true),
                              new KeyValuePair<string, bool>("No", false),
                          };
            return answers;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetGenders()
        {
            var genders = new[]
                          {
                              new KeyValuePair<string, string>("Male", "M"),
                              new KeyValuePair<string, string>("Female", "F"),
                              new KeyValuePair<string, string>("Not Specifying", "X")
                          };
            return genders;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetStates()
        {
            var states = new[]
                         {
                             new KeyValuePair<string, string>("ACT", "ACT"),
                             new KeyValuePair<string, string>("NSW", "NSW"),
                             new KeyValuePair<string, string>("NT", "NT"),
                             new KeyValuePair<string, string>("QLD", "QLD"),
                             new KeyValuePair<string, string>("SA", "SA"),
                             new KeyValuePair<string, string>("TAS", "TAS"),
                             new KeyValuePair<string, string>("VIC", "VIC"),
                             new KeyValuePair<string, string>("WA", "WA"),
                         };
            return states;
        }
    }
}