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
    public class DocumentTypeController : ApiControllerBase<DocumentType, DocumentTypeDto>
    {
        public DocumentTypeController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper, ILoggerManager logger) : base(pageService, dbContext, mapper, logger)
        {
        }

        [HttpGet]
        public override Task<IActionResult> GetAll(PageRequestFilter pageRequest)
        {
            return base.GetAll(pageRequest);
        }

        [HttpGet("{id}")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public override Task<IActionResult> Add(DocumentTypeDto tDto)
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
        public override Task<IActionResult> Update(int id, DocumentTypeDto tDto)
        {
            return base.Update(id, tDto);
        }

    }
}
