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

        /// <summary>
        /// Converts the string value to a mobile phone number format (xxxx xxx xxx).
        /// </summary>
        /// <param name="value">Mobile phone number value.</param>
        /// <returns>Returns the mobile phone number format.</returns>
        public static string ToMobile(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return value;
            }

            var formatted = value.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);
            if (formatted.Length != 10)
            {

                return value;
            }

            formatted = $"{formatted.Substring(0, 4)} {formatted.Substring(4, 3)} {formatted.Substring(7, 3)}";
            return formatted;
        }

        /// <summary>
        /// Converts the string value to a phone number format (xx xxxx xxxx).
        /// </summary>
        /// <param name="value">Mobile phone number value.</param>
        /// <returns>Returns the mobile phone number format.</returns>
        public static string ToPhone(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return value;
            }

            var formatted = value.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);
            if (formatted.Length != 10)
            {

                return value;
            }

            formatted = $"{formatted.Substring(0, 2)} {formatted.Substring(2, 4)} {formatted.Substring(6, 4)}";
            return formatted;
        }
    }
}