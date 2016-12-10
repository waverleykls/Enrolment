using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels.Generators
{
    public static class ContactItemsGenerator
    {
        public static IEnumerable<KeyValuePair<string, string>> GetRelationships()
        {
            var relationships = new[]
                                {
                                    new KeyValuePair<string, string>("Father", "Father"),
                                    new KeyValuePair<string, string>("Mother", "Mother"),
                                    new KeyValuePair<string, string>("Guardian", "Guardian"),
                                };
            return relationships;
        }
    }
}