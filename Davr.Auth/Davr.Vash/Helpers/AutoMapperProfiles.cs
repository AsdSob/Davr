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
            CreateMap<CurrencyDto, Currency>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<CurrencyRate, CurrencyRateDto>();
            CreateMap<CurrencyRateDto, CurrencyRate>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<Citizen, CitizenDto>();
            CreateMap<CitizenDto, Citizen>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<DocumentTypeDto, DocumentType>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<ExchangeTransaction, ExchangeTransactionDto>();
            CreateMap<ExchangeTransactionDto, ExchangeTransaction>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(x => x.Id, y => y.Ignore());

        }

    }
}
