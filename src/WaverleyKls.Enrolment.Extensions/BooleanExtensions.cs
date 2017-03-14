namespace WaverleyKls.Enrolment.Extensions
{
    /// <summary>
    /// This represents the extension entity for <see cref="bool"/>.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts the value to string of lower characters rather than capitalised one.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the converted string value.</returns>
        public static string ToLower(this bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// Converts the value to string of lower characters rather than capitalised one.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the converted string value.</returns>
        public static string ToLowerInvariant(this bool value)
        {
            return value.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Converts the value to HTML attribute of <c>checked</c>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the converted string value.</returns>
        public static string ToChecked(this bool value)
        {
            return value ? "checked=\"checked\"" : string.Empty;
        }

        /// <summary>
        /// Converts the value to HTML attribute of <c>selected</c>.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Returns the converted string value.</returns>
        public static string ToSelected(this bool value)
        {
            return value ? "selected=\"selected\"" : string.Empty;
        }
    }
}