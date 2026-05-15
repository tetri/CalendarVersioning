using System;
using Xunit;
using CalendarVersioning;

namespace CalendarVersioning.Tests.UnitTests
{
    public class CalendarVersionFormatTests
    {
        [Fact]
        public void Constructor_WithPattern_SetsPatternProperty()
        {
            // Arrange
            var pattern = "YYYY.MM.DD";

            // Act
            var format = new CalendarVersionFormat(pattern);

            // Assert
            Assert.Equal(pattern, format.Pattern);
        }

        [Theory]
        [InlineData("YYYY.MM", 2025, 4, null, null, "2025.04")]
        [InlineData("YY.MM.DD", 2025, 4, 29, null, "25.04.29")]
        [InlineData("YYYY.MM.DD.Minor", 2025, 4, 29, 1, "2025.04.29.1")]
        [InlineData("YYYY-MM-DD", 2025, 4, 29, null, "2025-04-29")]
        public void Format_WithValidPatterns_ReturnsExpectedStrings(string pattern, int year, int month, int? day, int? minor, string expected)
        {
            // Arrange
            var format = new CalendarVersionFormat(pattern);
            var version = new CalendarVersion(year, month, day, minor, format);

            // Act
            var result = format.Format(version);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Format_MissingDay_ThrowsInvalidOperationException()
        {
            // Arrange
            var format = new CalendarVersionFormat("YYYY.MM.DD");
            var version = new CalendarVersion(2025, 4, day: null, minor: null, format: format);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => format.Format(version));
        }

        [Fact]
        public void Format_MissingMinor_ThrowsInvalidOperationException()
        {
            // Arrange
            var format = new CalendarVersionFormat("YYYY.MM.Minor");
            var version = new CalendarVersion(2025, 4, day: 29, minor: null, format: format);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => format.Format(version));
        }
    }
}
