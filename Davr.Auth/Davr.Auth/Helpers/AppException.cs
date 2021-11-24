using System;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Davr.Auth.Helpers
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation) 
    // that can be caught and handled within the application
    public class AppException : Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    //public class ApiExceptionFilter : IExceptionFilter
    //{
    //    public ApiExceptionFilter()
    //    {

    //    }

            

    //    public void OnException(ExceptionContext context)
    //    {
    //        ApiError apiError;
    //        var ex = GetContextException(context);
    //        var httpContext = context.HttpContext;
    //        if (ex is UnauthorizedAccessException)
    //            apiError = new ApiError(HttpStatusCode.Unauthorized);
    //        else if (ex is BaseApiException baseException)
    //        {
    //            apiError = new ApiError(baseException.StatusCode)
    //            {
    //                Message = baseException.Message
    //            };
    //        }
    //        else
    //        {
    //            apiError = new ApiError(HttpStatusCode.InternalServerError);

    //            // TODO: Send email to admin
    //        }

    //        apiError.Path = httpContext.Request.Path;
    //        apiError.InitErrorText();

    //        httpContext.Response.ContentType = "application/json";
    //        httpContext.Response.StatusCode = apiError.Status;
    //        context.Result = new JsonResult(apiError);
    //    }

    //    private static Exception GetContextException(ExceptionContext context)
    //    {
    //        var ex = context.Exception;
    //        if (ex is BaseLogicException logicEx)
    //        {
    //            var message = context.HttpContext.TranslateContext(logicEx.Expression, logicEx.Args);
    //            if (ex is LogicInvalidOperationException)
    //                ex = new ApiBadRequestException(message);
    //            else if (ex is LogicNotFoundException)
    //                ex = new ApiNotFoundException(message);
    //            else
    //                ex = new Exception("Not supported exception", ex);
    //        }
    //        return ex;
    //    }
    //}
}