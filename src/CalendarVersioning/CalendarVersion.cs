using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CalendarVersioning
{
    [JsonConverter(typeof(CalendarVersionConverter))]
    public sealed class CalendarVersion : IComparable<CalendarVersion>
    {
        public int Year { get; }
        public int Month { get; }
        public int? Day { get; }
        public int? Minor { get; }

        public CalendarVersionFormat? Format { get; init; }

        public CalendarVersion(int year, int month, int? day = null, int? minor = null, CalendarVersionFormat? format = null)
        {
            Validate(year, month, day, minor, format);

            Year = year;
            Month = month;
            Day = day;
            Minor = minor;
            Format = format;
        }

        public static CalendarVersion Parse(string input, CalendarVersionFormat? format = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or empty", nameof(input));


            int year = 0, month = 0;
            int? day = null, minor = null;

            var parts = input.Split('.');
            if (format == null)
            {
                // Default parsing: YYYY.MM[.DD[.Minor]]
                if (parts.Length is < 2 or > 4)
                    throw new FormatException($"Version string '{input}' does not match default format 'YYYY.MM[.DD[.Minor]]'");

                year = int.Parse(parts[0]);
                month = int.Parse(parts[1]);
                day = parts.Length > 2 ? int.Parse(parts[2]) : null;
                minor = parts.Length > 3 ? int.Parse(parts[3]) : null;

                return new CalendarVersion(year, month, day, minor);
            }

            // Parsing using the format
            string pattern = format.Pattern;
            var tokens = pattern.Split('.');
            if (tokens.Length != parts.Length)
                throw new FormatException($"Version string '{input}' does not match format '{pattern}'");

            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                var value = int.Parse(parts[i]);

                switch (token)
                {
                    case "YYYY":
                        year = value;
                        break;
                    case "YY":
                        if (value is < 0 or > 99)
                            throw new FormatException($"Invalid YY value '{parts[i]}' in version string '{input}'");
                        year = 2000 + value;
                        break;
                    case "MM":
                        month = value;
                        break;
                    case "DD":
                        day = value;
                        break;
                    case "Minor":
                        minor = value;
                        break;
                    default:
                        throw new FormatException($"Unknown format token '{token}' in pattern '{pattern}'");
                }
            }

            return new CalendarVersion(year, month, day, minor, format);
        }

        public override string ToString()
        {
            if (Format != null)
                return Format.Format(this);

            // Fallback default formatting
            if (Day.HasValue && Minor.HasValue)
                return $"{Year:D4}.{Month:D2}.{Day.Value:D2}.{Minor.Value}";
            else if (Day.HasValue)
                return $"{Year:D4}.{Month:D2}.{Day.Value:D2}";
            else if (Minor.HasValue)
                throw new InvalidOperationException("Minor cannot be formatted without Day using the default format. Provide a CalendarVersionFormat (e.g. 'YYYY.MM.Minor') or include Day.");
            else
                return $"{Year:D4}.{Month:D2}";
        }

        // Equality and Comparison
        public override bool Equals(object? obj)
        {
            return obj is CalendarVersion other &&
                   Year == other.Year &&
                   Month == other.Month &&
                   Day == other.Day &&
                   Minor == other.Minor;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, Day, Minor);
        }

        public int CompareTo(CalendarVersion? other)
        {
            if (other is null) return 1;

            int result = Year.CompareTo(other.Year);
            if (result != 0) return result;

            result = Month.CompareTo(other.Month);
            if (result != 0) return result;

            result = Nullable.Compare(Day, other.Day);
            if (result != 0) return result;

            return Nullable.Compare(Minor, other.Minor);
        }

        public static bool operator ==(CalendarVersion? left, CalendarVersion? right) =>
            Equals(left, right);

        public static bool operator !=(CalendarVersion? left, CalendarVersion? right) =>
            !Equals(left, right);

        public static bool operator <(CalendarVersion? left, CalendarVersion? right)
        {
            if (ReferenceEquals(left, right)) return false;
            if (left is null) return right is not null;
            if (right is null) return false;
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(CalendarVersion? left, CalendarVersion? right)
        {
            if (ReferenceEquals(left, right)) return false;
            if (right is null) return left is not null;
            if (left is null) return false;
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(CalendarVersion? left, CalendarVersion? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null) return true;
            if (right is null) return false;
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(CalendarVersion? left, CalendarVersion? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (right is null) return true;
            if (left is null) return false;
            return left.CompareTo(right) >= 0;
        }

        private static void Validate(int year, int month, int? day, int? minor, CalendarVersionFormat? format)
        {
            if (year < 0 || year > 9999)
                throw new ArgumentOutOfRangeException(nameof(year), year, "Year must be between 0 and 9999.");

            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), month, "Month must be between 1 and 12.");

            if (day.HasValue)
            {
                int maxDay = DateTime.DaysInMonth(year == 0 ? 1 : year, month);
                if (day.Value < 1 || day.Value > maxDay)
                    throw new ArgumentOutOfRangeException(nameof(day), day, $"Day must be between 1 and {maxDay} for {year:D4}-{month:D2}.");
            }

            if (minor.HasValue && minor.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(minor), minor, "Minor must be >= 0.");

            // Default ToString / default Parse are ambiguous for Minor without Day.
            if (minor.HasValue && !day.HasValue && format is null)
                throw new ArgumentException("Minor requires Day when no format is provided (default format is YYYY.MM[.DD[.Minor]]).");
        }
    }


    public class CalendarVersionConverter : JsonConverter<CalendarVersion>
    {
        private readonly CalendarVersionFormat? _format;

        public CalendarVersionConverter() : this(null) { }

        public CalendarVersionConverter(CalendarVersionFormat? format)
        {
            _format = format;
        }

        public override CalendarVersion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException($"Expected JSON string or null when deserializing {nameof(CalendarVersion)}.");

            var stringValue = reader.GetString();
            if (string.IsNullOrWhiteSpace(stringValue))
                return null;

            return CalendarVersion.Parse(stringValue, _format);
        }

        public override void Write(Utf8JsonWriter writer, CalendarVersion value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}