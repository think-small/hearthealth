using Ardalis.GuardClauses;
using System;

namespace HeartHealth.Domain.ValueObjects
{
    public class DateRange
    {
        public DateTime Start { get; }
        public DateTime End { get; }
        public DateRange(DateTime start, DateTime end)
        {
            if (start > end) throw new ArgumentException("End date cannot precede start date.");

            Start = Guard.Against.OutOfSQLDateRange(start, nameof(start));
            End = Guard.Against.OutOfSQLDateRange(end, nameof(end));
        }

        public bool Has(DateTime other)
        {
            return other >= Start && other <= End;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DateRange;
            if (other is null) return false;

            return Start == other.Start && End == other.End;
        }

        public static bool operator == (DateRange dateRange, DateRange otherDateRange)
        {
            if (dateRange is null && otherDateRange is null) return true;
            if (dateRange is null || otherDateRange is null) return false;

            return dateRange.Equals(otherDateRange);
        }

        public static bool operator != (DateRange dateRange, DateRange otherDateRange)
        {
            return !dateRange.Equals(otherDateRange);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Start.GetHashCode();
                hash = hash * 23 + End.GetHashCode();

                return hash;
            }
        }
    }
}
