using System;

namespace WaverleyKls.Enrolment.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether the value is null or white space, or not.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Returns <c>True</c>, if the value is null or white space; otherwise returns <c>False</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            var result = string.IsNullOrWhiteSpace(value);

            return result;
        }

        /// <summary>
        /// Converts the base64-encoded string to <see cref="Guid"/>.
        /// </summary>
        /// <param name="base64EncodedValue">Base64-encoded string value to convert.</param>
        /// <returns>Returns the <see cref="Guid"/> converted.</returns>
        public static Guid ToGuid(this string base64EncodedValue)
        {
            if (base64EncodedValue.IsNullOrWhiteSpace())
            {
                return Guid.Empty;
            }

            var result = new Guid(Convert.FromBase64String(base64EncodedValue));

            return result;
        }
    }
}