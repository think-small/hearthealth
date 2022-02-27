using MediatR;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Commands
{
    public class AddBloodPressureMeasurementCommand : IRequest<AddBloodPressureMeasurementResponse>
    {
        public int Systolic { get; set; }
        public int Diastolic{ get; set; }
    }
}
