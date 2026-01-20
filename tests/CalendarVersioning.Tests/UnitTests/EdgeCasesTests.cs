using System;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class EdgeCasesTests
    {
        [Fact]
        public void Parse_MinimalVersion_ShouldSucceed()
        {
            var version = CalendarVersion.Parse("2025.01");
            Assert.Equal(2025, version.Year);
            Assert.Equal(1, version.Month);
        }

        [Fact]
        public void Parse_InvalidFormat_ShouldThrow()
        {
            Assert.Throws<FormatException>(() => CalendarVersion.Parse("invalid"));
        }

        [Fact]
        public void ToString_WithIncompleteFormat_ShouldThrow()
        {
            var version = new CalendarVersion(2025, 4, format: new CalendarVersionFormat("YYYY.MM.DD"));

            var ex = Assert.Throws<InvalidOperationException>(() => version.ToString());
            Assert.Contains("Day is required", ex.Message);
        }

        [Fact]
        public void Parse_WithZeroMinor_ShouldSucceed()
        {
            var version = CalendarVersion.Parse("2025.04.01.0");
            Assert.Equal(0, version.Minor);
        }

        [Fact]
        public void ToString_DefaultFormat_YearMonth_ShouldPad()
        {
            var version = new CalendarVersion(2025, 4);
            Assert.Equal("2025.04", version.ToString());
        }

        [Fact]
        public void ToString_DefaultFormat_YearMonthDay_ShouldPad()
        {
            var version = new CalendarVersion(2025, 4, day: 1);
            Assert.Equal("2025.04.01", version.ToString());
        }

        [Fact]
        public void ToString_DefaultFormat_YearMonthDayMinor_ShouldWork()
        {
            var version = new CalendarVersion(2025, 4, day: 1, minor: 7);
            Assert.Equal("2025.04.01.7", version.ToString());
        }

        [Fact]
        public void ToString_DefaultFormat_MinorWithoutDay_ShouldThrow()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CalendarVersion(2025, 4, day: null, minor: 1));
            Assert.Contains("Minor requires Day", ex.Message);
        }

        [Fact]
        public void Constructor_NegativeMinor_ShouldThrowOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CalendarVersion(2025, 4, day: 1, minor: -1));
        }
    }

}