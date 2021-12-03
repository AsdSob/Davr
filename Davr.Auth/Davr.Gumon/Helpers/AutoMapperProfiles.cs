using AutoMapper;
using Davr.Gumon.DTOs;
using Davr.Gumon.Entities;

namespace Davr.Gumon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>().ForMember(x => x.Id, y => y.Ignore());

            CreateMap<TransactionStatus, TransactionStatusDto>();
            CreateMap<TransactionStatusDto, TransactionStatus>().ForMember(x => x.Id, y => y.Ignore());

        }

    }
}
