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
