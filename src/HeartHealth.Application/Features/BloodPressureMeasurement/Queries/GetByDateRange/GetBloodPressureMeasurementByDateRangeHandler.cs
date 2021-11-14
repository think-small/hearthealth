using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeartHealth.Application.Features.BloodPressureMeasurement.Queries.GetByDateRange
{
    class GetBloodPressureMeasurementByDateRangeHandler : IRequestHandler<GetBloodPressureMeasurementByDateRangeQuery, GetBloodPressureMeasurementByDateRangeResponse>
    {
        private readonly IMapper _mapper;
        public GetBloodPressureMeasurementByDateRangeHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<GetBloodPressureMeasurementByDateRangeResponse> Handle(GetBloodPressureMeasurementByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetBloodPressureMeasurementByDateRangeResponse());
        }
    }
}
