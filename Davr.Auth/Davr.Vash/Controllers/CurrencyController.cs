using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Vash.Controllers.Abstracts;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;
using Davr.Vash.Helpers;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Davr.Vash.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ApiControllerBase<Currency,CurrencyDto>
    {
        protected readonly IPageResponseService _pageResponseService;
        protected readonly IDataAccessProvider _dbContext;
        protected readonly IMapper _mapper;

        public CurrencyController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {
            _pageResponseService = pageService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequestModel pageRequest)
        {
            var pageResponse = _pageResponseService.GetPageResponse<CurrencyDto>(pageRequest);

            var models = _dbContext._context.Currencies
                .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                .Take(pageResponse.PageSize);

            var dtos = _mapper.Map<IList<CurrencyDto>>(models).ToArray();

            pageResponse.Total = await _dbContext._context.Currencies.CountAsync();
            pageResponse.Items = dtos;

            return Ok(pageResponse);
        }


        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById( int id)
        //{
        //    var model = _dbContext.Currencies.Find(id);

        //    if (model == null) throw new Exception("Not exist");

        //    var dto = _mapper.Map<CurrencyDto>(model);

        //    return Ok(dto);
        //}

        //[HttpPost]
        //public virtual async Task<IActionResult> Add([FromBody] CurrencyDto tDto)
        //{
        //    var model = _mapper.Map<Currency>(tDto);

        //    _dbContext.Currencies.Add(model);
        //    _dbContext.SaveChanges();

        //    return Ok();
        //}

        //[HttpPut]
        //public virtual async Task<IActionResult> Update([FromBody] CurrencyDto tDto)
        //{
        //    var model = _mapper.Map<Currency>(tDto);

        //    _dbContext.Currencies.Update(model);
        //    _dbContext.SaveChanges();

        //    return Ok();
        //}

        //[HttpDelete("{id}")]
        //public virtual async Task<IActionResult> Delete(int id)
        //{
        //    var model = await _dbContext.FindAsync<Currency>(id);

        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    _dbContext.Currencies.Remove(model);
        //    _dbContext.SaveChanges();

        //    return Ok();
        //}

    }
}
