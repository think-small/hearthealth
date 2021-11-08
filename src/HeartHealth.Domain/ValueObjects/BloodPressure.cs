using Ardalis.GuardClauses;

namespace HeartHealth.Domain.ValueObjects
{
    public class BloodPressure
    {
        public int Systolic { get; }
        public int Diastolic { get; }
        public string Units { get; } = "mm Hg";

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
