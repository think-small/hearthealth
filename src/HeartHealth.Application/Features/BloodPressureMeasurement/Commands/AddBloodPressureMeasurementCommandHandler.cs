using AutoMapper;
using HeartHealth.Application.Contracts.Persistence;
using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using HeartHealth.Domain.Entities;
using HeartHealth.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Commands
{
    public class AddBloodPressureMeasurementCommandHandler : IRequestHandler<AddBloodPressureMeasurementCommand, AddBloodPressureMeasurementResponse>
    {
        private readonly IHistoriesRepository _historiesRepository;
        private readonly IMapper _mapper;
        public AddBloodPressureMeasurementCommandHandler(IHistoriesRepository historiesRepository, IMapper mapper)
        {
            _historiesRepository = historiesRepository;
            _mapper = mapper;
        }
        public async Task<AddBloodPressureMeasurementResponse> Handle(AddBloodPressureMeasurementCommand request, CancellationToken cancellationToken)
        {
            var response = new AddBloodPressureMeasurementResponse();

            try 
            {
                var today = DateTime.UtcNow;
                var weekAgo = today.AddDays(-7);
                var measurement = new Measurement
                {
                    Timestamp = today,
                    BloodPressure = new BloodPressure(request.Systolic, request.Diastolic)
                };
                
                var history = await _historiesRepository.GetBetween(weekAgo, today);
                var isRepeatNeeded = history.AddMeasurement(measurement);
                await _historiesRepository.Save(history);

                response.IsRepeatNeeded = isRepeatNeeded;
                response.Measurement = _mapper.Map<MeasurementDto>(measurement);
                response.Id = measurement.Id;
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
                response.WasSuccessful = false;
            }

            return response;
        }
    }
}
