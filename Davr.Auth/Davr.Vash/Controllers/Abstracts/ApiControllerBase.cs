using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Vash.Authorization;
using Davr.Vash.DataAccess;
using Davr.Vash.Entities.Abstracts;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Vash.Controllers.Abstracts
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ApiControllerBase<TModel, TDto> : ControllerBase where TModel : class, IEntity<int>
    {
        protected readonly IPageResponseService _pageResponseService;
        protected readonly IDataAccessProvider _dbContext;
        protected readonly IMapper _mapper;


        public ApiControllerBase(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper)
        {
            _pageResponseService = pageService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //sample of query with array object 
        //localhost:44398/Currency/?pagesize=5&filters[0].f=name&filters[0].v=[lk]a&ilters[1].f=id&filters[1].v=[eq]a

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        {
            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<TDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<TModel>();

            //Get entities filtered with expression
            //var models = _dbContext._context.Currencies.Skip((pageResponse.Page - 1) * pageResponse.PageSize).Take(pageResponse.PageSize);
            var models = await _dbContext.GetEntities(
                expression,
                (pageResponse.Page - 1) * pageResponse.PageSize,
                pageResponse.PageSize);

            var dtos = _mapper.Map<IList<TDto>>(models).ToArray();

            pageResponse.Total = _dbContext.GetEntitiesCount<TModel>(expression);

            pageResponse.Items = dtos;

            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var model = await _dbContext.GetEntity<TModel>(id);

            if (model == null) return NotFound();
            
            var dto = _mapper.Map<TDto>(model);

            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TDto tDto)
        {
            var model = _mapper.Map<TModel>(tDto);
            await _dbContext.AddOrUpdateEntity(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] TDto tDto)
        {
            if (tDto == null)
            {
                return BadRequest("Owner object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var entity = _dbContext.GetEntity<TModel>(id).Result;

            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map(tDto, entity);
            await _dbContext.AddOrUpdateEntity(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await _dbContext.GetEntity<TModel>(id);

            if (model == null)
            {
                return NotFound();
            }

            await _dbContext.DeleteEntity(model);
            return Ok();
        }
    }
}
