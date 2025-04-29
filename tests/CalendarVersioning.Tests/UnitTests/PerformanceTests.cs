using System.Diagnostics;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class PerformanceTests
    {
        [Fact]
        public void Parse_LargeAmountOfVersions_ShouldBeFast()
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                var version = CalendarVersion.Parse($"2025.04.{i % 30 + 1}.{i % 5}");
            }

            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 500); // Arbitrário: 10.000 parses em < 500ms
        }
    }
}