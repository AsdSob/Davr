using System.Threading.Tasks;
using AutoMapper;
using Davr.Gumon.Authorization;
using Davr.Gumon.Controllers.Abstracts;
using Davr.Gumon.DataAccess;
using Davr.Gumon.DTOs;
using Davr.Gumon.Entities;
using Davr.Gumon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Gumon.Controllers
{
    public class BranchController : ApiControllerBase<Branch, BranchDto>
    {
        public BranchController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {

        }

        [HttpGet]
        public override Task<IActionResult> GetAll(PageRequestFilter pageRequest)
        {
            return base.GetAll(pageRequest);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public override Task<IActionResult> Add(BranchDto tDto)
        {
            return base.Add(tDto);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(int id, BranchDto tDto)
        {
            return base.Update(id, tDto);
        }
    }
}
