namespace WebWeatherApi.Framework_and_drivers.Mapper
{
    using AutoMapper;
    using WebWeatherApi.Entities.Model;
    using WebWeatherApi.Interface_Adapters.DTO;

    public class WeatherMappingProfile : Profile
    {
        public WeatherMappingProfile()
        {

            CreateMap<WeatherRecordDetails, WeatherRecordDetailsDTO>().ReverseMap();

            CreateMap<WeatherRecord, WeatherRecordDTO>()
               .ForMember(dest => dest.WeatherRecordDetails, opt => opt.MapFrom(src => src.WeatherRecordDetails))
               .ReverseMap();
        }
    }
}
