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
        private readonly IJwtService _jwtService;
        public AuthController(IRepository<User> userRepository, IAuthService authService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _authService.GetUserByLogin(dto.UserNameOrEmail, dto.Password);
            string jwtToken = _jwtService.Generate(user.Id, user.UsersClaims.Select(c => c.ToSecurityClaim()));

            Response.Cookies.Append("token", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                messsage = ResponseMessage.Success,
                value = new { token = jwtToken }
            });
        }

        [ServiceFilter(typeof(Authorization))]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var jwtToken = Request.Cookies["token"];
            var token = _jwtService.Verify(jwtToken);
            int userId = int.Parse(token.Issuer);
            var user = _userRepository.Get(userId);

            return Ok(user);
        }

        [HttpPost("loggout")]
        public IActionResult Loggout()
        {
            Response.Cookies.Delete("token");
            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }
    }
}
