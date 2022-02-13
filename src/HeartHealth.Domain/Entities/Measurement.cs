using HeartHealth.Domain.Shared;
using HeartHealth.Domain.ValueObjects;
using System;

namespace HeartHealth.Domain.Entities
{
    public class Measurement : BaseEntity
    {
        public DateTime Timestamp { get; set; }
        public BloodPressure BloodPressure { get; set; }
        public bool RequiresVerification { get; set; }
    }
}
