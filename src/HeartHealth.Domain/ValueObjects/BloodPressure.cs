using Ardalis.GuardClauses;
using HeartHealth.Domain.Shared;

namespace HeartHealth.Domain.ValueObjects
{
    public class BloodPressure
    {
        public int Systolic { get; }
        public int Diastolic { get; }
        public string Units { get; } = "mm Hg";
        public Stages Stage
        {
            get
            {
                if (Systolic >= 180 || Diastolic >= 120) return Stages.Crisis;
                if (Systolic >= 140 || Diastolic >= 90) return Stages.Stage2;
                if (Systolic >= 130 || Diastolic >= 80) return Stages.Stage1;
                if (Systolic >= 120 && Diastolic < 80) return Stages.Elevated;

                return Stages.Normal;
            }
        }

        public BloodPressure(int systolic, int diastolic)
        {
            Systolic = Guard.Against.NegativeOrZero(systolic, nameof(systolic));
            Diastolic = Guard.Against.NegativeOrZero(diastolic, nameof(diastolic));
        }

        public override bool Equals(object obj)
        {
            var other = obj as BloodPressure;
            if (other is null) return false;

            return Systolic == other.Systolic
                   && Diastolic == other.Diastolic
                   && Units == other.Units;
        }

        public static bool operator == (BloodPressure pressure, BloodPressure otherPressure)
        {
            if (pressure is null && otherPressure is null) return true;
            if (pressure is null || otherPressure is null) return false;

            return pressure.Equals(otherPressure);
        }

        public static bool operator != (BloodPressure pressure, BloodPressure otherPressure)
        {
            return !(pressure == otherPressure);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + Systolic.GetHashCode();
                hash = hash * 23 + Diastolic.GetHashCode();
                hash = hash * 23 + Units.GetHashCode();
                return hash;
            }
        }
    }
}
