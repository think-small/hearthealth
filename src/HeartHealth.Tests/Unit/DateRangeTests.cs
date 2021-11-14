using HeartHealth.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class DateRangeTests
    {        
        [Test, TestCaseSource(typeof(InvalidDateTimes), nameof(InvalidDateTimes.Dates))]
        public void Invalid_SqlDateTime_Will_Throw(DateTime start, DateTime end)
        {
            Action act = () => new DateRange(start, end);

            act.Should().Throw<ArgumentException>();
        }
    }
    class InvalidDateTimes
    {
        internal static object[] Dates =
        {
            //  C# DateTime.MinValue is 1/1/0001
            //  T-SQL Date minimum value is 1/1/1753
            new object[] { DateTime.MinValue, DateTime.UtcNow },

            // C# and T-SQL measure time in different base units.
            // C# uses 100 nano sec increments vs T-SQL uses 3.33 mili sec
            new object[] { DateTime.UtcNow, DateTime.MaxValue },

            new object[] { DateTime.UtcNow, new DateTime(2000, 1, 1) }
        };
    }
}
