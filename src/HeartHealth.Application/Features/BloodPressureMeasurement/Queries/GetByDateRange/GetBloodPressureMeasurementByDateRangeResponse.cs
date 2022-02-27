using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using System.Collections.Generic;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries
{
    public class GetBloodPressureMeasurementByDateRangeResponse
    {
        public IEnumerable<MeasurementDto> Measurements { get; set; }
    }
}