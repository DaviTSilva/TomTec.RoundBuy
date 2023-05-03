using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.API.Records.v1;
using TomTec.RoundBuy.Business.Validations;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1
{
    [Route("v1/profiles")]
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

        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto dto)
        {
            var user = dto.ToModel();
            user.Validate();
            var createdUser = _userRepository.Create(dto.ToModel());
            return Created(ResponseMessage.Success, new UserRegisterRecord(createdUser));
        }

        [HttpGet("")]
        [Authorize]
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
        [Authorize]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.Get(u => u.Id == id && u.Active == true,
                    nameof(Models.User.UsersClaims),
                    $"{nameof(Models.User.UsersClaims)}.{nameof(UsersClaims.Claim)}",
                    nameof(Models.User.Address),
                    nameof(Models.User.OfficialIdentificationType),
                    nameof(Models.User.Image)
                );
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpGet("username/{userName}")]
        [Authorize]
        public IActionResult GetUserByUserName(string userName)
        {
            var user = _userRepository.Get(u => u.UserName == userName && u.Active == true,
                    nameof(Models.User.UsersClaims),
                    $"{nameof(Models.User.UsersClaims)}.{nameof(UsersClaims.Claim)}",
                    nameof(Models.User.Address),
                    nameof(Models.User.OfficialIdentificationType),
                    nameof(Models.User.Image)
                ).FirstOrDefault();

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpGet("email/{email}")]
        [Authorize]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _userRepository.Get(u => u.Email == email && u.Active == true,
                    nameof(Models.User.UsersClaims),
                    $"{nameof(Models.User.UsersClaims)}.{nameof(UsersClaims.Claim)}",
                    nameof(Models.User.Address),
                    nameof(Models.User.OfficialIdentificationType),
                    nameof(Models.User.Image)
                ).FirstOrDefault();

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = user
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.Get(id,
                    nameof(Models.User.UsersClaims),
                    $"{nameof(Models.User.UsersClaims)}.{nameof(UsersClaims.Claim)}",
                    nameof(Models.User.Address),
                    nameof(Models.User.OfficialIdentificationType),
                    nameof(Models.User.Image)
                ); 

            user.Active = false;
            _userRepository.Update(user);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPost("restore/{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult RestoreDeletedUser(int id)
        {
            var user = _userRepository.Get(id,
                    nameof(Models.User.UsersClaims),
                    $"{nameof(Models.User.UsersClaims)}.{nameof(UsersClaims.Claim)}",
                    nameof(Models.User.Address),
                    nameof(Models.User.OfficialIdentificationType),
                    nameof(Models.User.Image)
                );

            user.Active = true;
            _userRepository.Update(user);
            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateProfileDto dto, int id)
        {
            User user = dto.ToModel();
            user.Id = id;

            user.Validate();
            var initialUser = _userRepository.Get(id, nameof(Models.User.UsersClaims));
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
