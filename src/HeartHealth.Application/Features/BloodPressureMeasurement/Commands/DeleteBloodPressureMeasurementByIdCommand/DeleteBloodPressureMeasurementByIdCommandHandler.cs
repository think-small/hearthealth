using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Commands
{
    public class DeleteBloodPressureMeasurementByIdCommandHandler : IRequestHandler<DeleteBloodPressureMeasurementByIdCommand, DeleteBloodPressureMeasurementByIdResponse>
    {
        public Task<DeleteBloodPressureMeasurementByIdResponse> Handle(DeleteBloodPressureMeasurementByIdCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
