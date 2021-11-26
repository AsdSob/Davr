using AutoMapper;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;

namespace Davr.Vash.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Currency, CurrencyDto>().ReverseMap();

            CreateMap<CurrencyRate, CurrencyRateDto>().ReverseMap();

            CreateMap<Citizen, CitizenDto>().ReverseMap();

            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();

            CreateMap<Branch, BranchDto>().ReverseMap();

            CreateMap<Client, ClientDto>();
        }

    }
}
