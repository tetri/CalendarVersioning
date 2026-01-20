using System.Collections.Generic;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class EqualityAndHashTests
    {
        [Fact]
        public void Equals_SameComponents_ShouldBeTrue()
        {
            var a = new CalendarVersion(2025, 4, 1, 2);
            var b = new CalendarVersion(2025, 4, 1, 2);

            Assert.True(a.Equals(b));
            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact]
        public void Equals_DifferentComponents_ShouldBeFalse()
        {
            var a = new CalendarVersion(2025, 4, 1, 2);
            var b = new CalendarVersion(2025, 4, 1, 3);

            Assert.False(a.Equals(b));
            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact]
        public void GetHashCode_EqualObjects_ShouldMatch()
        {
            var a = new CalendarVersion(2025, 4, 1, 2);
            var b = new CalendarVersion(2025, 4, 1, 2);

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact]
        public void HashSet_ShouldTreatEqualVersionsAsSameKey()
        {
            var set = new HashSet<CalendarVersion>
            {
                new CalendarVersion(2025, 4, 1, 2),
                new CalendarVersion(2025, 4, 1, 2),
            };

            Assert.Single(set);
        }

        [Fact]
        public void Equals_ShouldIgnoreFormat()
        {
            var withFormat = new CalendarVersion(2025, 4, day: 1, minor: 2, format: new CalendarVersionFormat("YYYY.MM.DD.Minor"));
            var withoutFormat = new CalendarVersion(2025, 4, day: 1, minor: 2);

            Assert.Equal(withoutFormat, withFormat);
        }
    }
}
