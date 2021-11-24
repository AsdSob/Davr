using AutoMapper;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;

namespace Davr.Vash.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>();

            CreateMap<CurrencyRate, CurrencyRateDto>();
            CreateMap<CurrencyRateDto, CurrencyRate>();

            CreateMap<Citizen, CitizenDto>();
            CreateMap<CitizenDto, Citizen>();

            CreateMap<DocumentTypeDto, DocumentType>();
            CreateMap<DocumentType, DocumentTypeDto>();

        }
    }
}
