using MediatR;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries
{
    public class GetBloodPressureMeasurementByIdQuery : IRequest<GetBloodPressureMeasurementByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
