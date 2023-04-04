using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;

namespace TomTec.RoundBuy.API.Controllers.v1.Sales
{
    [Route("v1/payment-methods")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class PaymentMethodController : Controller
    {
        public IActionResult CratePaymmentMethod()
        {
            return View();
        }
    }
}
