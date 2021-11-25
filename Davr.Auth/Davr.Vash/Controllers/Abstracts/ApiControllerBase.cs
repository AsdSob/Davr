using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Vash.DataAccess;
using Davr.Vash.DTOs;
using Davr.Vash.Entities.Abstracts;
using Davr.Vash.Services;
using Microsoft.AspNetCore.Mvc;

namespace Davr.Vash.Controllers.Abstracts
{
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


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        {
            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<TModel>();

            //Get entities filtered with expression
            var models = await _dbContext.GetEntities(expression);

            //var models = _dbContext._context.Currencies
            //    .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
            //    .Take(pageResponse.PageSize);

            var dtos = _mapper.Map<IList<TDto>>(models).ToArray();


            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<TDto>(pageRequest);

            if (expression == null)
            {
                pageResponse.Total = _dbContext.GetEntitiesCount<TModel>();
            }
            else
            {
                pageResponse.Total = _dbContext.GetEntitiesCount<TModel>(expression);
            }

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

        //[HttpPost]
        //public virtual async Task<IActionResult> AddRange([FromBody] IList<TDto> tDto)
        //{
        //    var models = _mapper.Map<IList<TModel>>(tDto);
        //    await _dbContext.AddOrUpdateEntities(models);
        //    return Ok();
        //}

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TDto tDto)
        {
            var model = _mapper.Map<TModel>(tDto);
            await _dbContext.AddOrUpdateEntity(model);
            return Ok();
        }

        //[HttpPut]
        //public virtual async Task<IActionResult> UpdateRange([FromBody] IList<TDto> tDto)
        //{
        //    var models = _mapper.Map<IList<TModel>>(tDto);
        //    await _dbContext.AddOrUpdateEntities(models);
        //    return Ok();
        //}


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

        //[HttpDelete]
        //public virtual async Task<IActionResult> DeleteRange([FromBody] IList<TDto> tDto)
        //{
        //    var model = _mapper.Map<IList<TModel>>(tDto);

        //    await _dbContext.DeleteEntities(model);
        //    return Ok();
        //}
    }
}
