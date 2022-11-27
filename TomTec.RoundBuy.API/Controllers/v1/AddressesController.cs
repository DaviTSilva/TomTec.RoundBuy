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
    [Route("v1/addresses")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class AddressesController : Controller
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressesController(IRepository<Address> addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        [HttpGet("")]
        [Authorize]
        public IActionResult GetAddresses()
        {
            var adresses = _addressRepository.Get();
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = adresses
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetAddress(int id)
        {
            var address = _addressRepository.Get(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = address
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateAdress([FromBody] AddressDto dto, int id)
        {
            var address = dto.ToModel();
            address.Id = id;
            _addressRepository.Update(address);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }
    }
}
