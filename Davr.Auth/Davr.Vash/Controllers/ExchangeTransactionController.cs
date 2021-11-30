using System;
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
using Microsoft.EntityFrameworkCore;

namespace Davr.Vash.Controllers
{
    public class ExchangeTransactionController : ApiControllerBase<ExchangeTransaction, ExchangeTransactionDto>
    {
        public ExchangeTransactionController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> GetAll(PageRequestFilter pageRequest)
        {
            // Access by role
            var currentUser = (User)HttpContext.Items["User"];

            var roleFilter = new FieldFilter();

            if (currentUser.Role == Role.Supervisor)
            {
                roleFilter = new FieldFilter() {f = "branchId", v = "[eq]" + currentUser.BranchId.ToString()};
            }
            else if(currentUser.Role == Role.User)
            {
                roleFilter = new FieldFilter() {f = "userId", v = "[eq]" + currentUser.BranchId.ToString()};
            }

            if (!String.IsNullOrWhiteSpace(roleFilter.f))
            {
                pageRequest.filters = pageRequest.filters == null
                    ? new FieldFilter[] {roleFilter}
                    : pageRequest.filters.Concat(new[] {roleFilter}).ToArray();
            }

            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<ExchangeTransactionDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<ExchangeTransaction>();

            //Get entities filtered with expression

            var models = new List<ExchangeTransaction>();

            if (expression == null)
            {
                models = _dbContext._context.ExchangeTransactions
                    .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                    .Take(pageResponse.PageSize)
                    .Include(x => x.Branch)
                    .Include(x => x.Client)
                    .Include(x => x.Currency)
                    .Include(x => x.User).ToList();
            }
            else
            {
                models = _dbContext._context.ExchangeTransactions
                    .Where(expression)
                    .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                    .Take(pageResponse.PageSize)
                    .Include(x => x.Branch)
                    .Include(x => x.Client)
                    .Include(x => x.Currency)
                    .Include(x => x.User).ToList();
            }


            var dtos = _mapper.Map<IList<ExchangeTransactionDto>>(models).ToArray();

            pageResponse.Total = _dbContext.GetEntitiesCount<ExchangeTransaction>(expression);

            pageResponse.Items = dtos;

            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(ExchangeTransactionDto tDto)
        {
            var exTran = _mapper.Map<ExchangeTransaction>(tDto);

            if (tDto.Client == null) return BadRequest("Client object is not set ");

            if (tDto.Client.Id == 0)
            {
                exTran.Client = _mapper.Map<Client>(tDto.Client);
            }
            else
            {
                exTran.Client.Id = tDto.Client.Id;
                var updateClient = _mapper.Map<Client>(tDto.Client);
                updateClient.Id = exTran.Client.Id;
                exTran.Client = updateClient;
            }

            await _dbContext.AddOrUpdateEntity(exTran);

            return Ok();
            //return base.Add(tDto);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public override async Task<IActionResult> Update(int id, ExchangeTransactionDto tDto)
        {
            if (tDto == null) return BadRequest("Owner object is null");

            if (tDto.Client == null) return BadRequest("Client object is not set ");

            if (!ModelState.IsValid) return BadRequest("Invalid model object");

            var entity = _dbContext.GetEntity<ExchangeTransaction>(id).Result;

            if (entity == null)
            {
                return NotFound();
            }

            var exTran = _mapper.Map(tDto, entity);

            var updateClient = _mapper.Map<Client>(tDto.Client);
            updateClient.Id = exTran.Client.Id;
            exTran.Client = updateClient;

            await _dbContext.AddOrUpdateEntity(exTran);

            return Ok();
            //return base.Update(id, tDto);
        }
    }
}
