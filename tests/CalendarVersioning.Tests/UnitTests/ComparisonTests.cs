using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    using Xunit;

    public class ComparisonTests
    {
        [Fact]
        public void Compare_EqualVersions_ShouldReturnZero()
        {
            var v1 = CalendarVersion.Parse("2025.04.29.1");
            var v2 = CalendarVersion.Parse("2025.04.29.1");

            Assert.Equal(0, v1.CompareTo(v2));
        }

        [Fact]
        public void Compare_LessThan_ShouldReturnNegative()
        {
            var older = CalendarVersion.Parse("2025.04");
            var newer = CalendarVersion.Parse("2025.04.01");

            Assert.True(older.CompareTo(newer) < 0);
        }

        [Fact]
        public void Compare_GreaterThan_ShouldReturnPositive()
        {
            var newer = CalendarVersion.Parse("2025.04.01");
            var older = CalendarVersion.Parse("2025.04");

            Assert.True(newer.CompareTo(older) > 0);
        }
    }

}