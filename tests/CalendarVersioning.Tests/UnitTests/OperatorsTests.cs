using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class OperatorTests
    {
        [Fact]
        public void EqualityOperators_ShouldWork()
        {
            var v1 = CalendarVersion.Parse("2025.04");
            var v2 = CalendarVersion.Parse("2025.04");

            Assert.True(v1 == v2);
            Assert.False(v1 != v2);
        }

        [Fact]
        public void ComparisonOperators_ShouldWork()
        {
            var older = CalendarVersion.Parse("2025.04");
            var newer = CalendarVersion.Parse("2025.04.01");

            Assert.True(older < newer);
            Assert.True(newer > older);
            Assert.True(older <= newer);
            Assert.True(newer >= older);
        }

        [Fact]
        public void Operators_WithNulls_ShouldBeConsistent()
        {
            CalendarVersion? left = null;
            CalendarVersion? right = CalendarVersion.Parse("2025.04");

            Assert.True(left == null);
            Assert.False(left != null);

            Assert.True(left < right);
            Assert.False(left > right);

            Assert.True(left <= right);
            Assert.False(left >= right);

            Assert.False(right <= left);
            Assert.True(right >= left);
        }
    }

}