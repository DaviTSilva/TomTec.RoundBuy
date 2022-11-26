using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.Business;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Lib.Utils;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1
{
    [Route("v1/auth")]
    [AllowAnonymous]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class AuthController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAuthService _authService;
        private readonly ITokenService _jwtService;
        public AuthController(IRepository<User> userRepository, IAuthService authService, ITokenService jwtService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _authService.GetUserByLogin(dto.UserNameOrEmail, dto.Password);
            string jwtToken = _jwtService.Generate(user.Id, _authService.GenerateSecurityClaims(user));

            return Ok(new
            {
                messsage = ResponseMessage.Success,
                value = new { token = jwtToken }
            });
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var userName = User.Identity.Name;
            var user = _userRepository.Get(u => u.UserName.Equals(userName)).FirstOrDefault();

            return Ok(user);
        }

        //[HttpPost("loggout")]
        //public IActionResult Loggout()
        //{
        //    Response.Cookies.Delete("token");
        //    return Ok(new
        //    {
        //        message = ResponseMessage.Success
        //    });
        //}
    }
}
