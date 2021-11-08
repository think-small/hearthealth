using HeartHealth.Domain.ValueObjects;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models
{
    public class MeasurementDto
    {
        public DateTime Timestamp { get; set; }
        public BloodPressure BloodPressure { get; set; }
    }
}
