using AutoMapper;
using CathayInterviewAPI.Models.Requests;

namespace CathayInterviewAPI.Mappings
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CreateCurrencyDto, Currency>();
            CreateMap<Currency, CurrencyDto>()
                .ForMember(dest => dest.CurrencyName, opt => opt.MapFrom(src => src.CurrencyCode));
            CreateMap<CurrencyDto, Currency>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyName));

        }
    }
}
