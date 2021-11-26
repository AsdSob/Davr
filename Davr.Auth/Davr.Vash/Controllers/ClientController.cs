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
    public class ClientController : ApiControllerBase<Client, ClientDto>
    {
        public ClientController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {

        }

        [HttpGet]
        public override async Task<IActionResult> GetAll([FromQuery] PageRequestFilter pageRequest)
        {
            //Set page response
            var pageResponse = _pageResponseService.GetPageResponse<ClientDto>(pageRequest);

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<Client>();

            //Get entities filtered with expression
            var models = _dbContext._context.Clients.Skip((pageResponse.Page - 1) * pageResponse.PageSize)
                .Take(pageResponse.PageSize).Include(x => x.DocumentType).Include(x => x.Citizen);

            var dtos = _mapper.Map<IList<ClientDto>>(models).ToArray();

            pageResponse.Total = _dbContext.GetEntitiesCount<Client>(expression);

            pageResponse.Items = dtos;

            return Ok(pageResponse);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpPost]
        public override Task<IActionResult> Add(ClientDto tDto)
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
        public override Task<IActionResult> Update(int id, ClientDto tDto)
        {
            return base.Update(id, tDto);
        }
    }
}
