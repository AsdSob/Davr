using System.Collections.Generic;
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
        private readonly ICurrencyRateCBService _currencyCBRate;
        private readonly IMapper _mapper;
        private readonly IDataAccessProvider _dbContext;
        private readonly ILoggerManager _logger;

        public CurrencyController(ILoggerManager logger, ICurrencyRateCBService currencyCbRate, IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper, logger)
        {
            _currencyCBRate = currencyCbRate;
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public async override Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        {
            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<CurrencyDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<Currency>();

            var currencies = await _dbContext.GetEntities(
                expression,
                (pageResponse.Page - 1) * pageResponse.PageSize,
                pageResponse.PageSize);

            var currenciesDto = _mapper.Map<List<CurrencyDto>>(currencies);

            foreach (var dto in currenciesDto.Where(x => x.Code != "860"))
            {
                _logger.LogInfo(dto.Code + dto.Name + dto.Rate);

                dto.Rate = await _currencyCBRate.GetCurrency(dto.Code);
            }

            pageResponse.Total = _dbContext.GetEntitiesCount<Currency>(expression);
            pageResponse.Items = currenciesDto.ToArray();

            return Ok(pageResponse);

            //_logger.LogInfo("running controler base");

            //return base.GetAll(pageRequest);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> Get(int id)
        {
            //var currency = _dbContext.GetEntity<Currency>(id).Result;

            //var dto = _mapper.Map<CurrencyDto>(currency);

            //if (dto.Code == "860")
            //    return Ok(dto);
            //try
            //{
            //    dto.Rate = await _currencyCBRate.GetCurrency(dto.Code);
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e);
            //}

            //return Ok(dto);

            return base.Get(id);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public override async Task<IActionResult> Add(CurrencyDto tDto)
        {
            if (_dbContext._context.Currencies.Any(x => x.Name.ToUpper() == tDto.Name.ToUpper() || x.Code == tDto.Code))
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
