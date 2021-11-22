using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using HeartHealth.Application.Responses;
using System;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Commands
{
    public class AddBloodPressureMeasurementResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public MeasurementDto Measurement { get; set; }
        public bool IsRepeatNeeded { get; set; }
    }
}
