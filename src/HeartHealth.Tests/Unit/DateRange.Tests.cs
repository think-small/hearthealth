using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;

namespace HeartHealth.Tests.Unit
{
    [TestFixture]
    public class DateRange
    {
        [TestCaseSource(typeof(InvalidDateTimes))]
        public void Invalid_SqlDateTime_Will_Throw(Tuple<DateTime, DateTime> dates)
        {
            Action act = () => new Domain.ValueObjects.DateRange(dates.Item1, dates.Item2);

            act.Should().Throw<ArgumentException>();
        }
    }

    internal class InvalidDateTimes : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            //  C# DateTime.MinValue is 1/1/0001
            //  T-SQL Date minimum value is 1/1/1753
            yield return new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.UtcNow);

            // C# and T-SQL measure time in different base units.
            // C# uses 100 nano sec increments vs T-SQL uses 3.33 mili sec
            yield return new Tuple<DateTime, DateTime>(DateTime.UtcNow, DateTime.MaxValue);

            yield return new Tuple<DateTime, DateTime>(DateTime.UtcNow, new DateTime(2000, 1, 1));
        }
    }
}
