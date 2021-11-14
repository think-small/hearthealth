using AutoMapper;
using HeartHealth.Application.Contracts.Persistence;
using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using HeartHealth.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries.GetById
{
    public class GetBloodPressureMeasurementByIdQueryHandler : IRequestHandler<GetBloodPressureMeasurementByIdQuery, GetBloodPressureMeasurementByIdResponse>
    {
        private readonly IBaseRepository<Measurement> _repository;
        private readonly IMapper _mapper;
        public GetBloodPressureMeasurementByIdQueryHandler(IBaseRepository<Measurement> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GetBloodPressureMeasurementByIdResponse> Handle(GetBloodPressureMeasurementByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetBloodPressureMeasurementByIdResponse();
            
            try
            {
                var measurement = await _repository.GetById(request.Id);
                if (measurement is null)
                {
                    response.WasSuccessful = false;
                    response.AddError($"No measurement found with id {request.Id}");
                }
                response.Measurement = _mapper.Map<MeasurementDto>(measurement);
            }
            catch (Exception e)
            {
                response.WasSuccessful = false;
                response.AddError(e.Message);
            }

            return response;
        }
    }
}
