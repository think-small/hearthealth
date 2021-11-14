using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using HeartHealth.Application.Responses;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries.GetById
{
    public class GetBloodPressureMeasurementByIdResponse : BaseResponse
    {
        public MeasurementDto Measurement{ get; set; }
    }
}
