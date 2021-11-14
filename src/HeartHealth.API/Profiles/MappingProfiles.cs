using AutoMapper;
using HeartHealth.API.ViewModels;
using HeartHealth.Application.Features.BloodPressureMeasurement.Commands;
using HeartHealth.Application.Features.BloodPressureMeasurement.Queries.GetById;

namespace HeartHealth.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BloodPressureVM, AddBloodPressureMeasurementCommand>();
            CreateMap<GetBloodPressureMeasurementByIdResponse, MeasurementVM>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Measurement.Timestamp.ToLocalTime()))
                .ForMember(dest => dest.Systolic, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Systolic))
                .ForMember(dest => dest.Diastolic, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Diastolic))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Units))
                .ForAllOtherMembers(src => src.Ignore());
            CreateMap<AddBloodPressureMeasurementResponse, MeasurementVM>()
                .ForMember(dest => dest.Systolic, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Systolic))
                .ForMember(dest => dest.Diastolic, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Diastolic))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Measurement.BloodPressure.Units))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Measurement.Timestamp.ToLocalTime()))
                .ForAllOtherMembers(src => src.Ignore());
        }
    }
}
