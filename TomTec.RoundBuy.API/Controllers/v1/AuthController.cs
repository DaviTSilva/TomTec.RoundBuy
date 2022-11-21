using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
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
        private readonly IRepository<Claim> _claimRepository;
        private readonly JwtService _jwtService;
        public AuthController(IRepository<User> userRepository, IRepository<Claim> claimRepository)
        {
            _userRepository = userRepository;
            _claimRepository = claimRepository;
            _jwtService = new JwtService();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _userRepository.Get(
                u => u.Email.Equals(dto.UserNameOrEmail) || u.UserName.Equals(dto.UserNameOrEmail),
                u => u.UsersClaims
                ).FirstOrDefault();
            user.UsersClaims.ToList().ForEach(c => c.Claim = _claimRepository.Get(c.ClaimId));


            if (user == null)
                return BadRequest(new { message = "Invalid Credentials!" });
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(new { message = "Invalid Credentials!" });

            string jwtToken = _jwtService.Generate(user.Id, user.UsersClaims.Select(c => c.ToSecurityClaim()));
            Response.Cookies.Append("token", jwtToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                messsage = ResponseMessage.Success
            });
        }

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
