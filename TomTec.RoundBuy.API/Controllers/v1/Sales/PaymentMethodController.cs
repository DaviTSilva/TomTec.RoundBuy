using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1
{
    [Route("v1/payment-methods")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class PaymentMethodController : Controller
    {
        private readonly IRepository<PaymentMethod> _paymentMethodRepository;

        public PaymentMethodController(IRepository<PaymentMethod> paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        [HttpPost("")]
        [Authorize(Roles = "manager")]
        public IActionResult CratePaymmentMethod([FromBody] PaymentMethodDto paymentModelDto)
        {
            var paymentMethod = _paymentMethodRepository.Create(paymentModelDto.ToModel());
            return Created(ResponseMessage.Success, paymentMethod);
        }

        [HttpGet("")]
        public IActionResult GetPaymentMethods()
        {
            var paymentMethods = _paymentMethodRepository.Get();
            return Ok(new {
                message = ResponseMessage.Success,
                value = paymentMethods,
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentMethodById(int id)
        {
            var paymentMethod = _paymentMethodRepository.Get(id);
            return Ok(new { 
                message = ResponseMessage.Success,
                value = paymentMethod,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult UpdatePaymentMethod(int id, [FromBody]PaymentMethodDto paymentMethodDto)
        {
            var paymentMethod = paymentMethodDto.ToModel();
            paymentMethod.Id = id;
            _paymentMethodRepository.Update(paymentMethod);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeletePaymentMethod(int id)
        {
            _paymentMethodRepository.Delete(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }
    }
}
