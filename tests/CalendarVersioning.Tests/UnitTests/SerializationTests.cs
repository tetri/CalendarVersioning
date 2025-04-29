﻿using System;
using System.Text.Json;

using Xunit;

namespace CalendarVersioning.Tests.UnitTests
{
    public class SerializationTests
    {
        [Fact]
        public void SerializeAndDeserialize_ShouldBeEqual()
        {
            var original = new CalendarVersion(2025, 4, 29, 1);
            var json = JsonSerializer.Serialize(original);
            var deserialized = JsonSerializer.Deserialize<CalendarVersion>(json);

            Assert.Equal(original, deserialized);
        }

        [Fact]
        public void Deserialize_InvalidJson_ShouldThrow()
        {
            var json = "\"invalid.version\"";

            Assert.Throws<FormatException>(() => JsonSerializer.Deserialize<CalendarVersion>(json));
        }
    }
}