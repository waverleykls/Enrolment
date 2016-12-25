using System;

namespace WaverleyKls.Enrolment.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            var result = string.IsNullOrWhiteSpace(value);
            return result;
        }

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