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
    }

}