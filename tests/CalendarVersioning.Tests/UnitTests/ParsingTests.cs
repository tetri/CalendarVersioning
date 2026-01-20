using System;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class ParsingTests
    {
        [Fact]
        public void Parse_Null_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => CalendarVersion.Parse(null!));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        public void Parse_Whitespace_ShouldThrowArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => CalendarVersion.Parse(input));
        }

        [Fact]
        public void Parse_FullVersion_ShouldSucceed()
        {
            var version = CalendarVersion.Parse("2025.04.29.1");

            Assert.Equal(2025, version.Year);
            Assert.Equal(4, version.Month);
            Assert.Equal(29, version.Day);
            Assert.Equal(1, version.Minor);
        }

        [Fact]
        public void Parse_CustomFormat_YYMM_ShouldWork()
        {
            var format = new CalendarVersionFormat("YY.MM");
            var version = CalendarVersion.Parse("25.04", format);

            Assert.Equal(2025, version.Year);
            Assert.Equal(4, version.Month);
        }

        [Fact]
        public void Parse_WithExtraComponents_ShouldThrow()
        {
            Assert.Throws<FormatException>(() => CalendarVersion.Parse("2025.04.29.error"));
        }

        [Theory]
        [InlineData("2025")]
        [InlineData("2025.04.01.2.3")]
        public void Parse_DefaultFormat_WrongNumberOfParts_ShouldThrowFormatException(string input)
        {
            Assert.Throws<FormatException>(() => CalendarVersion.Parse(input));
        }

        [Theory]
        [InlineData("2025.00")]
        [InlineData("2025.13")]
        public void Parse_InvalidMonth_ShouldThrowOutOfRange(string input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CalendarVersion.Parse(input));
        }

        [Theory]
        [InlineData("2025.02.29")] // 2025 não é bissexto
        [InlineData("2025.04.31")]
        [InlineData("2025.01.00")]
        [InlineData("2025.01.32")]
        public void Parse_InvalidDay_ShouldThrowOutOfRange(string input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CalendarVersion.Parse(input));
        }

        [Fact]
        public void Parse_LeapYearDay_ShouldSucceed()
        {
            var version = CalendarVersion.Parse("2024.02.29");
            Assert.Equal(2024, version.Year);
            Assert.Equal(2, version.Month);
            Assert.Equal(29, version.Day);
        }

        [Fact]
        public void Parse_CustomFormat_TokenCountMismatch_ShouldThrow()
        {
            var format = new CalendarVersionFormat("YYYY.MM.DD");
            Assert.Throws<FormatException>(() => CalendarVersion.Parse("2025.04", format));
        }

        [Fact]
        public void Parse_CustomFormat_UnknownToken_ShouldThrow()
        {
            var format = new CalendarVersionFormat("YYYY.MM.PATCH");
            Assert.Throws<FormatException>(() => CalendarVersion.Parse("2025.04.1", format));
        }

        [Fact]
        public void Parse_CustomFormat_InvalidYY_ShouldThrowFormatException()
        {
            var format = new CalendarVersionFormat("YY.MM");
            Assert.Throws<FormatException>(() => CalendarVersion.Parse("123.04", format));
        }
    }
}