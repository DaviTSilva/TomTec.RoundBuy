using Microsoft.AspNetCore.Authorization;
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
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1.Sales
{
    [Route("v1/ratings")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class RatingsController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IRatingService _ratingService;

        public RatingsController(IAuthService authService, IRepository<Rating> ratingRepository, IRatingService ratingService)
        {
            _authService = authService;
            _ratingRepository = ratingRepository;
            _ratingService = ratingService;
        }

        [HttpPost("")]
        public IActionResult CreateRating([FromBody] RatingDto ratingDto)
        {
            var userId = _authService.GetCurrentUser(User).Id;
            _ratingService.CheckIfUserCanRate(userId, ratingDto.AnnouncementId);
            var rating = ratingDto.ToModel(userId);
            rating.Validate();
            var createdRating = _ratingRepository.Create(rating);

            return Created(ResponseMessage.Success, createdRating);
        }

        [HttpGet("{id}")]
        public IActionResult GetRatingById(int id)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _ratingRepository.Get(id, c => c.AuthorUser)
            });
        }

        [HttpGet("announcement/{announcementId}")]
        public IActionResult GetRatingByAnnouncement(int announcementId)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _ratingRepository.Get(c => c.AnnouncementId == announcementId, c => c.AuthorUser)
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRating([FromBody] UpdateRatingDto ratingDto, int id)
        {
            var rating = _ratingRepository.Get(id);
            ratingDto.UpdateModel(rating);

            if (rating.AuthorUserId != _authService.GetCurrentUser(User).Id)
                throw new UnauthorizedAccessException();

            _ratingRepository.Update(rating);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult DeleteRating(int id)
        {
            _ratingRepository.Delete(id);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }
    }
}
