using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Davr.Gumon.DataAccess;
using Davr.Gumon.Entities;
using Davr.Gumon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Davr.Gumon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly IDataAccessProvider _dbContext;
        private readonly IReportService _reportService;

        public TestController(IDataAccessProvider dataAccess, IReportService reportService)
        {
            _dbContext = dataAccess;
            _reportService = reportService;
        }

        [HttpPost("[action]")]
        public virtual async Task<IActionResult> TransactionList(PageRequestFilter pageRequest)
        {
            var transactions = GetTransactionSuspiciouses(pageRequest);
            var report = _reportService.GenerateTransactionListReport(transactions);

            return File(report.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Gumon-" + DateTime.Now.ToShortDateString() + ".xlsx");
        }

        private List<TransactionSuspicious> GetTransactionSuspiciouses(PageRequestFilter pageRequest)
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

            //Convert filter array to expression
            var expression = pageRequest.filters.FiltersToExpression<TransactionSuspicious>();

            var models = new List<TransactionSuspicious>();
            if (expression == null)
            {
                models = _dbContext._context.TransactionSuspiciouses
                    .Include(x => x.Branch)
                    .Include(x => x.User)
                    .Include(x => x.OperationCriteria)
                    .Include(x => x.Currency)
                    .ToList();
            }
            else
            {
                models = _dbContext._context.TransactionSuspiciouses
                    .Where(expression)
                    .Include(x => x.Branch)
                    .Include(x => x.User)
                    .Include(x => x.OperationCriteria)
                    .Include(x => x.Currency)
                    .ToList();
            }

            return models;
        }
    }
}
