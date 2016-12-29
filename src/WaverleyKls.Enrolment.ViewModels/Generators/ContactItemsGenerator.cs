using System.Collections.Generic;

namespace WaverleyKls.Enrolment.ViewModels.Generators
{
    /// <summary>
    /// This represents the generator entity used for contact view models.
    /// </summary>
    public static class ContactItemsGenerator
    {
        /// <summary>
        /// Gets the list of relationships.
        /// </summary>
        /// <returns>Returns the list of relationships.</returns>
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