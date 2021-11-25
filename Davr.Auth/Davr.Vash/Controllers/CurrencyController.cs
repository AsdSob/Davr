using AutoMapper;
using Davr.Vash.Controllers.Abstracts;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;
using Davr.Vash.Services;

namespace Davr.Vash.Controllers
{
    public class CurrencyController : ApiControllerBase<Currency,CurrencyDto>
    {
        public CurrencyController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {

        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        //{
        //    var pageResponse = _pageResponseService.GetPageResponse<CurrencyDto>(pageRequest);

        //    var expression = pageRequest.filters.FiltersToExpression<Currency>();

        //    var models = _dbContext._context.Currencies
        //        .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
        //        .Take(pageResponse.PageSize);

        //    var dtos = _mapper.Map<IList<CurrencyDto>>(models).ToArray();

        //    pageResponse.Total = await _dbContext._context.Currencies.CountAsync();
        //    pageResponse.Items = dtos;

        //    return Ok(pageResponse);
        //}

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
