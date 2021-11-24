using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class CurrencyController : ControllerBase
    {
        protected readonly IPageResponseService _pageResponseService;
        protected readonly DataContext _dbContext;
        protected readonly IMapper _mapper;

        public CurrencyController(IPageResponseService pageService, DataContext dbContext, IMapper mapper)
        {
            _pageResponseService = pageService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequestModel pageRequest)
        {
            var models = _dbContext.Currencies
                .Skip((pageRequest.page - 1) * pageRequest.pagesize)
                .Take(pageRequest.pagesize);

            var totalItems = _dbContext.Currencies.CountAsync();

            var dtos = _mapper.Map<IList<CurrencyDto>>(models).ToArray();

            var pageResponse = _pageResponseService.GetPageResponse<CurrencyDto>(dtos, totalItems.Result, pageRequest);

            return Ok(pageResponse);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            var model = _dbContext.Currencies.Find(id);

            return Ok(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] CurrencyDto tDto)
        {
            var model = _mapper.Map<Currency>(tDto);

            _dbContext.Currencies.Add(model);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] CurrencyDto tDto)
        {
            var model = _mapper.Map<Currency>(tDto);

            _dbContext.Currencies.Update(model);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromBody] CurrencyDto tDto)
        {
            var model = _mapper.Map<Currency>(tDto);

            _dbContext.Currencies.Remove(model);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
