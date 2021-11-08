using AutoMapper;
using HeartHealth.Application.Features.BloodPressureMeasurement.Shared.Models;
using HeartHealth.Domain.Entities;

namespace HeartHealth.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Measurement, MeasurementDto>();
        }
    }
}
