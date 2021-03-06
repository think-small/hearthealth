using MediatR;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries
{
    public class GetBloodPressureMeasurementByDateRangeQuery : IRequest<GetBloodPressureMeasurementByDateRangeResponse>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
