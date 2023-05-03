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
    [Route("v1/user-types")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class UserTypesController : Controller
    {
        private readonly IRepository<UserType> _userTypeRepository;

        public UserTypesController(IRepository<UserType> userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        //[HttpPost("")]
        //[Authorize(Roles = "manager")]
        //public IActionResult CreateUserType([FromBody] UserTypeDto dto)
        //{
        //    var userType = _userTypeRepository.Create(dto.ToModel());

        //    return Created(ResponseMessage.Success, userType);
        //}

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetUserTypes()
        {
            var userTypes = _userTypeRepository.Get();

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = userTypes,
            }); ;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetUserType(int id)
        {
            var userType = _userTypeRepository.Get(id);
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = userType
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult UpdateUserType([FromBody] UserTypeDto dto, int id)
        {
            var userType = dto.ToModel();
            userType.Id = id;
            _userTypeRepository.Update(userType);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "manager")]
        //public IActionResult DeleteUserType(int id)
        //{
        //    _userTypeRepository.Delete(id);

        //    return Ok(new
        //    {
        //        message = ResponseMessage.Success
        //    });
        //}
    }
}
