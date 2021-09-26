using AutoMapper;
using WorldCities.Models.Dto;
using WorldCities.Models.Models;

namespace CompanyEmployees.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Company, CompanyDto>()
            //    .ForMember(c => c.FullAddress,
            //        opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<CityForUpdateDto, City>();
            CreateMap<CityForCreateDto, City>();
            CreateMap<City, CityDto>();

            CreateMap<CountryForUpdateDto, Country>();
            CreateMap<CountryForCreateDto, Country>();
            CreateMap<Country, CountryDto>();
        }
    }
}
