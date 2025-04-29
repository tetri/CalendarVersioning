using System;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class ParsingTests
    {
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
    }
}