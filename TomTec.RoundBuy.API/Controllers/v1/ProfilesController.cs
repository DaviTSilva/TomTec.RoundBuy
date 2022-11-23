using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.API.Records.v1;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1
{
    [Route("v1/profiles")]
    [ServiceFilter(typeof(Authorization))]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class ProfilesController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public ProfilesController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            var user = _userRepository.Create(dto.ToModel());
            return Created(ResponseMessage.Success, new UserRegisterRecord(user));
        }

        [HttpGet("")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.Get();
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = new UserListRecord(users)
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.Get(u => u.Id == id && u.Active == true);
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpGet("username/{userName}")]
        public IActionResult GetUserByUserName(string userName)
        {
            var user = _userRepository.Get(u => u.UserName == userName && u.Active == true).FirstOrDefault();
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userRepository.Get(u => u.Email == email && u.Active == true).FirstOrDefault();
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.Get(id);
            user.Active = false;
            _userRepository.Update(user);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPost("restore/{id}")]
        public IActionResult RestoreDeletedUser(int id)
        {
            var user = _userRepository.Get(id);
            user.Active = true;
            _userRepository.Update(user);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UpdateProfileDto dto, int id)
        {
            User user = dto.ToModel();
            user.Id = id;

            var initialUser = _userRepository.Get(id);
            user.Password = initialUser.Password;
            user.PasswordSalt = initialUser.PasswordSalt;
            _userRepository.Update(user);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }
    }
}
