using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Vash.Controllers.Abstracts;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Davr.Vash.Controllers
{
    public class CurrencyRateController : ApiControllerBase<CurrencyRate, CurrencyRateDto>
    {
        public CurrencyRateController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {
        }



        [HttpGet]
        public override async Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        {
            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<CurrencyRateDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<CurrencyRate>();

            //Get entities filtered with expression
            var models = _dbContext._context.CurrencyRates.Skip((pageResponse.Page - 1) * pageResponse.PageSize).Take(pageResponse.PageSize).Include(x=> x.Currency);

            var dtos = _mapper.Map<IList<CurrencyRateDto>>(models).ToArray();

            pageResponse.Total = _dbContext.GetEntitiesCount<CurrencyRate>(expression);

            pageResponse.Items = dtos;

            return Ok(pageResponse);
        }
    }
}
