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
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class OfficialIdentificationTypesController : Controller
    {
        private readonly IRepository<OfficialIdentificationType> _officialIdTypeRepository;

        public OfficialIdentificationTypesController(IRepository<OfficialIdentificationType> officialIdTypeRepository)
        {
            _officialIdTypeRepository = officialIdTypeRepository;
        }

        [HttpPost("")]
        [Authorize(Roles = "manager")]
        public IActionResult CreateOfficialIdType([FromBody] OfficialIdentificationTypeDto dto)
        {
            var officialIdType = _officialIdTypeRepository.Create(dto.ToModel());

            return Created(ResponseMessage.Success, officialIdType);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetOfficialIdType()
        {
            var officialIdTypes = _officialIdTypeRepository.Get();

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = officialIdTypes,
            }); ;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetOfficialIdType(int id)
        {
            var officialIdType = _officialIdTypeRepository.Get(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = officialIdType
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult UpdateOfficialIdType([FromBody] OfficialIdentificationTypeDto dto, int id)
        {
            var officialIdType = dto.ToModel();
            officialIdType.Id = id;
            _officialIdTypeRepository.Update(officialIdType);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeleteOfficialIdType(int id)
        {
            _officialIdTypeRepository.Delete(id);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }
    }
}
