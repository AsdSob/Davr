using AutoMapper;
using Davr.Vash.Controllers.Abstracts;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;
using Davr.Vash.Services;

namespace Davr.Vash.Controllers
{
    public class BranchController : ApiControllerBase<Branch, BranchDto>
    {
        public BranchController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {

        }
    }
}
