using MediatR;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries.GetById
{
    public class GetBloodPressureMeasurementByIdQuery : IRequest<GetBloodPressureMeasurementByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
