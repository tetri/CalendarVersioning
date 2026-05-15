using System;

namespace CalendarVersioning
{
    public sealed class CalendarVersionFormat
    {
        public string Pattern { get; }
        internal string[] Tokens { get; }

        public CalendarVersionFormat(string pattern)
        {
            Pattern = pattern;
            Tokens = pattern.Split('.', 10);
        }

        internal string Format(CalendarVersion version)
        {
            string result = Pattern;

            result = result.Replace("YYYY", version.Year.ToString("D4"));
            result = result.Replace("YY", (version.Year % 100).ToString("D2"));
            result = result.Replace("MM", version.Month.ToString("D2"));

            if (result.Contains("DD"))
            {
                if (!version.Day.HasValue)
                    throw new InvalidOperationException("Day is required by the format but not present in the version.");
                result = result.Replace("DD", version.Day.Value.ToString("D2"));
            }

            if (result.Contains("Minor"))
            {
                if (!version.Minor.HasValue)
                    throw new InvalidOperationException("Minor is required by the format but not present in the version.");
                result = result.Replace("Minor", version.Minor.Value.ToString());
            }

            return result;
        }
    }
}
