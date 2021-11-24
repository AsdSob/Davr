using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Davr.Auth.Helpers
{
    internal class AutoValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                throw new ValidationException();

        }
    }
}
