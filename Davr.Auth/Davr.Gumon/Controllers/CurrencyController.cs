﻿using AutoMapper;
using Davr.Gumon.Controllers.Abstracts;
using Davr.Gumon.DataAccess;
using Davr.Gumon.DTOs;
using Davr.Gumon.Entities;
using Davr.Gumon.Services;

namespace Davr.Gumon.Controllers
{
    public class CurrencyController : ApiControllerBase<Currency, CurrencyDto>
    {
        public CurrencyController(IPageResponseService pageService, IDataAccessProvider dbContext, IMapper mapper) : base(pageService, dbContext, mapper)
        {

        }
    }
}