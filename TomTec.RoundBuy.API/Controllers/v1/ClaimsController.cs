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
    public class ClaimsController : Controller
    {
        private readonly IRepository<Claim> _claimsRepository;
        private readonly IUsersClaimsRepository _userClaimsRepository;

        public ClaimsController(IRepository<Claim> uerRoleRepository)
        {
            _claimsRepository = uerRoleRepository;
        }

        [HttpPost("")]
        [Authorize(Roles = "manager")]
        public IActionResult CreateClaim([FromBody] ClaimDto dto)
        {
            var claim = _claimsRepository.Create(dto.ToModel());

            return Created(ResponseMessage.Success, claim);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetClaim()
        {
            var claim = _claimsRepository.Get(
                    nameof(Claim.UsersClaims),
                    $"{nameof(Claim.UsersClaims)}.{nameof(UsersClaims.User)}"
                );

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = claim
            });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetClaim(int id)
        {
            var claim = _claimsRepository.Get(id,
                    nameof(Claim.UsersClaims),
                    $"{nameof(Claim.UsersClaims)}.{nameof(UsersClaims.User)}"
                );

            return Ok(new
            {
                message = ResponseMessage.Success,
                value = claim,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult UpdateClaim([FromBody] ClaimDto dto, int id)
        {
            var claim = dto.ToModel();
            claim.Id = id;

            _claimsRepository.Update(claim);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeleteClaim(int id)
        {

            _claimsRepository.Delete(id);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPost("sign")]
        [Authorize(Roles = "manager")]
        public IActionResult SignClaim([FromBody] SigningClaimDto dto)
        {
            _userClaimsRepository.SignUserToClaim(dto.UserId, dto.ClaimId);
            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }

        [HttpPost("unsign")]
        [Authorize(Roles = "manager")]
        public IActionResult UnsignClaim([FromBody] SigningClaimDto dto)
        {
            _userClaimsRepository.UnsignUserToClaim(dto.UserId, dto.ClaimId);
            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }
    }
}
