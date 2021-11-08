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
        private readonly IBaseRepository<Measurement> _repository;
        private readonly IMapper _mapper;
        public AddBloodPressureMeasurementCommandHandler(IBaseRepository<Measurement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AddBloodPressureMeasurementResponse> Handle(AddBloodPressureMeasurementCommand request, CancellationToken cancellationToken)
        {
            var response = new AddBloodPressureMeasurementResponse();

            try 
            {
                var measurement = new Measurement
                {
                    Timestamp = DateTime.UtcNow,
                    BloodPressure = new BloodPressure(request.Systolic, request.Diastolic)
                };

                var savedEntity = await _repository.Add(measurement);
                response.Measurement = _mapper.Map<MeasurementDto>(savedEntity);
                response.Id = savedEntity.Id;
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
