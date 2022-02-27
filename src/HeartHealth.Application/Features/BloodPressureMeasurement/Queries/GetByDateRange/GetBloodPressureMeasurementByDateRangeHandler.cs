using AutoMapper;
using HeartHealth.Application.Contracts.Persistence;
using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries
{
    class GetBloodPressureMeasurementByDateRangeHandler : IRequestHandler<GetBloodPressureMeasurementByDateRangeQuery, GetBloodPressureMeasurementByDateRangeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IHistoriesRepository _historiesRepository;
        public GetBloodPressureMeasurementByDateRangeHandler(IMapper mapper, IHistoriesRepository historiesRepository)
        {
            _mapper = mapper;
            _historiesRepository = historiesRepository;
        }
        public async Task<GetBloodPressureMeasurementByDateRangeResponse> Handle(GetBloodPressureMeasurementByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var history = await _historiesRepository.GetBetweenAsync(request.Start, request.End);
            var measurements = _mapper.Map<List<MeasurementDto>>(history.Measurements).AsReadOnly();
            return new GetBloodPressureMeasurementByDateRangeResponse { Measurements = measurements };
        }
    }
}
