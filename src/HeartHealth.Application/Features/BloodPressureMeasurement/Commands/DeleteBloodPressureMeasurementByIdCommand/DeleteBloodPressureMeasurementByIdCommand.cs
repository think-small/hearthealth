using MediatR;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Commands
{
    public  class DeleteBloodPressureMeasurementByIdCommand : IRequest<DeleteBloodPressureMeasurementByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
