using System;

namespace WaverleyKls.Enrolment.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="Guid"/>.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Converts the <see cref="Guid"/> value to base64-encoded. string value.
        /// </summary>
        /// <param name="value"><see cref="Guid"/> value to convert.</param>
        /// <returns>Returns the base64-encoded stgring value.</returns>
        public static string ToBase64String(this Guid value)
        {
            var result = Convert.ToBase64String(value.ToByteArray());

            return result;
        }
    }
}