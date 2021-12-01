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
        public ExchangeTransactionController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper, ILoggerManager logger) : base(pageService, dbContext, mapper, logger)
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
                    .Include(x => x.Client).ThenInclude(x => x.Citizen)
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
                    .Include(x => x.Client).ThenInclude(x => x.Citizen)
                    .Include(x => x.Currency)
                    .Include(x => x.User).ToList();
            }

            var dtos = new List<ExchangeTransactionDto>();

            foreach (var model in models)
            {
                var dto = _mapper.Map<ExchangeTransactionDto>(model);

                dto.DocumentSeries = model.Client.DocumentSeries;
                dto.DocumentTypeId = model.Client.DocumentTypeId;
                dto.DocumentIssueDate = model.Client.DocumentIssueDate;
                dto.DocumentNumber = model.Client.DocumentNumber;
                dto.DocumentAuthority = model.Client.DocumentAuthority;
                dto.Name = model.Client.Name;
                dto.SurName = model.Client.SurName;
                dto.MiddleSurName = model.Client.MiddleSurName;
                dto.CitizenId = model.Client.CitizenId;
                dto.BirthPlace = model.Client.BirthPlace;
                dto.BirthDate = model.Client.BirthDate;
                dto.Registration = model.Client.Registration;
                dto.Citizen = _mapper.Map<CitizenDto>(model.Client.Citizen);

                dtos.Add(dto);
            }

            //var dtos = _mapper.Map<IList<ExchangeTransactionDto>>(models).ToArray();
           
            pageResponse.Total = _dbContext.GetEntitiesCount<ExchangeTransaction>(expression);

            pageResponse.Items = dtos.ToArray();

            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            var transaction = _dbContext._context.ExchangeTransactions
                .Where(x => x.Id == id)
                .Include(x => x.Branch)
                .Include(x => x.Client).ThenInclude(x=> x.Citizen)
                .Include(x => x.Currency)
                .Include(x => x.User).FirstOrDefault();

            var transactionDto = _mapper.Map<ExchangeTransactionDto>(transaction);

            transactionDto.DocumentSeries = transaction.Client.DocumentSeries;
            transactionDto.DocumentTypeId = transaction.Client.DocumentTypeId;
            transactionDto.DocumentIssueDate = transaction.Client.DocumentIssueDate;
            transactionDto.DocumentNumber = transaction.Client.DocumentNumber;
            transactionDto.DocumentAuthority = transaction.Client.DocumentAuthority;
            transactionDto.Name = transaction.Client.Name;
            transactionDto.SurName = transaction.Client.SurName;
            transactionDto.MiddleSurName = transaction.Client.MiddleSurName;
            transactionDto.CitizenId = transaction.Client.CitizenId;
            transactionDto.BirthPlace = transaction.Client.BirthPlace;
            transactionDto.BirthDate = transaction.Client.BirthDate;
            transactionDto.Registration = transaction.Client.Registration;
            transactionDto.Citizen = _mapper.Map<CitizenDto>(transaction.Client.Citizen);


            return Ok(transactionDto);
        }

        [HttpPost]
        public override async Task<IActionResult> Add(ExchangeTransactionDto tDto)
        {
            var exTran = _mapper.Map<ExchangeTransaction>(tDto);

            // Search client with document number and series in DB
            var existClient =_dbContext._context.Clients.FirstOrDefault(x =>
                x.DocumentNumber == tDto.DocumentNumber && x.DocumentSeries == tDto.DocumentSeries);

            var clientDto = _mapper.Map<ClientDto>(tDto);

            //Create new Client
            if (existClient == null || existClient.Id <= 0)
            {
                exTran.ClientId = 0;
                exTran.Client = _mapper.Map<Client>(clientDto);
            }
            else
            {
                //Update client details and set ClientId ofr exchange transaction
                exTran.ClientId = existClient.Id;
                exTran.Client = _mapper.Map(clientDto, existClient);
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

            if (!ModelState.IsValid) return BadRequest("Invalid model object");

            var entity = _dbContext.GetEntity<ExchangeTransaction>(id).Result;

            if (entity == null)
            {
                return NotFound();
            }

            var exTran = _mapper.Map(tDto, entity);

            // Search client with document number and series in DB
            var existClient = _dbContext._context.Clients.FirstOrDefault(x =>
                x.DocumentNumber == tDto.DocumentNumber && x.DocumentSeries == tDto.DocumentSeries);

            var clientDto = _mapper.Map<ClientDto>(tDto);

            //Update client details and set ClientId for exchange transaction
            exTran.Client = _mapper.Map(clientDto, existClient);

            await _dbContext.AddOrUpdateEntity(exTran);
            return Ok();
        }
    }
}
