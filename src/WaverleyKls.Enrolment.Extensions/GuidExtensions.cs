using System;

namespace WaverleyKls.Enrolment.Extensions
{
    public static class GuidExtensions
    {
        public static string ToBase64String(this Guid value)
        {
            var result = Convert.ToBase64String(value.ToByteArray());
            return result;
        }
    }
}