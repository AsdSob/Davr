using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Vash.Authorization;
using Davr.Vash.Controllers.Abstracts;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Vash.Controllers
{
    public class CurrencyController : ApiControllerBase<Currency,CurrencyDto>
    {
        public CurrencyController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
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
        public override async Task<IActionResult> Add(CurrencyDto tDto)
        {
            if (_dbContext._context.Currencies.Any(x => x.Name.ToUpper() == tDto.Name.ToUpper()))
            {
                return BadRequest($"{tDto.Name} Currency already exist");
            }

            return  await base.Add(tDto);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(int id, CurrencyDto tDto)
        {
            return base.Update(id, tDto);
        }

    }
}
