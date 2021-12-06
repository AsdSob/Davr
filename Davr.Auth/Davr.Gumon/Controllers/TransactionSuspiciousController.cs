using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Davr.Gumon.Authorization;
using Davr.Gumon.Controllers.Abstracts;
using Davr.Gumon.DataAccess;
using Davr.Gumon.DTOs;
using Davr.Gumon.Entities;
using Davr.Gumon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Davr.Gumon.Controllers
{
    public class TransactionSuspiciousController : ApiControllerBase<TransactionSuspicious, TransactionSuspiciousDto>
    {
        public TransactionSuspiciousController(IPageResponseService pageService, IDataAccessProvider dbContext,
            IMapper mapper) : base(pageService, dbContext, mapper)
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
                roleFilter = new FieldFilter() { f = "branchId", v = "[eq]" + currentUser.BranchId.ToString() };
            }
            else if (currentUser.Role == Role.User)
            {
                roleFilter = new FieldFilter() { f = "userId", v = "[eq]" + currentUser.Id.ToString() };
            }

            if (!String.IsNullOrWhiteSpace(roleFilter.f))
            {
                pageRequest.filters = pageRequest.filters == null
                    ? new FieldFilter[] { roleFilter }
                    : pageRequest.filters.Concat(new[] { roleFilter }).ToArray();
            }

            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<TransactionSuspiciousDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<TransactionSuspicious>();

            //Get entities filtered with expression

            var models = new List<TransactionSuspicious>();

            if (expression == null)
            {
                models = _dbContext._context.TransactionSuspiciouses
                    .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                    .Take(pageResponse.PageSize)
                    .Include(x => x.Branch)
                    .Include(x => x.Currency)
                    .Include(x => x.User)
                    .Include(x=> x.TransactionStatus)
                    .Include(x=> x.OperationCriteria)
                    .ToList();
            }
            else
            {
                models = _dbContext._context.TransactionSuspiciouses
                    .Where(expression)
                    .Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                    .Take(pageResponse.PageSize)
                    .Include(x => x.Branch)
                    .Include(x => x.Currency)
                    .Include(x => x.User)
                    .Include(x => x.TransactionStatus)
                    .Include(x => x.OperationCriteria)
                    .ToList();
            }
            var dtos = _mapper.Map<List<TransactionSuspiciousDto>>(models);
            pageResponse.Total = _dbContext.GetEntitiesCount<TransactionSuspicious>(expression);
            pageResponse.Items = dtos.ToArray();

            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var transaction = _dbContext._context.TransactionSuspiciouses
                .Where(x => x.Id == id)
                .Include(x => x.Branch)
                .Include(x => x.Currency)
                .Include(x => x.User)
                .Include(x => x.TransactionStatus)
                .Include(x => x.OperationCriteria)
                .ToList().FirstOrDefault();

            var transactionDto = _mapper.Map<TransactionSuspiciousDto>(transaction);
            return Ok(transactionDto);
        }

        [HttpPost]
        public override Task<IActionResult> Add(TransactionSuspiciousDto tDto)
        {
            return base.Add(tDto);
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(int id, TransactionSuspiciousDto tDto)
        {
            return base.Update(id, tDto);
        }
    }
}
