using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Lib.AspNetCore.Filters
{
    public class UnauthorizedAccessExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger<UnauthorizedAccessExceptionFilterAttribute> _logger;
        public UnauthorizedAccessExceptionFilterAttribute(ILogger<UnauthorizedAccessExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                _logger.LogError(new EventId(0), actionExecutedContext.Exception, actionExecutedContext.Exception.Message);
                actionExecutedContext.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
