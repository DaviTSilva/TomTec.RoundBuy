using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Lib.AspNetCore.Filters
{
    public class KeyNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger<KeyNotFoundExceptionFilterAttribute> _logger;
        public KeyNotFoundExceptionFilterAttribute(ILogger<KeyNotFoundExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is KeyNotFoundException)
            {
                _logger.LogError(new EventId(0), actionExecutedContext.Exception, actionExecutedContext.Exception.Message);
                actionExecutedContext.Result = new NotFoundResult();
                return;
            }
        }
    }
}
