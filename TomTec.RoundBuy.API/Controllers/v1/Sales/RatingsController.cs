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
        public RatingsController(IAuthService authService, IRepository<Rating> ratingRepository)
        {
            _authService = authService;
            _ratingRepository = ratingRepository;
        }

        [HttpPost("")]
        public IActionResult CreateRating([FromBody] RatingDto ratingDto)
        {
            int authorUserId = _authService.GetCurrentUser(User).Id;
            var rating = ratingDto.ToModel(authorUserId);
            CorretRating(rating);
            _ratingRepository.Create(rating);

            return Created(ResponseMessage.Success, rating);
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

        [HttpGet("announcement/{id}")]
        public IActionResult GetRatingByAnnouncement(int announcementId)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _ratingRepository.Get(c => c.AnnouncementId == announcementId, c => c.AuthorUser)
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromBody] UpdateCommentDto commentDto, int id)
        {
            var rating = _ratingRepository.Get(id);
            rating.CommentText = commentDto.CommentText;

            _ratingRepository.Update(rating);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _ratingRepository.Delete(id);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }

        private void CorretRating(Rating rating)
        {
            rating.Rate = rating.Rate > 5
                ? 5
                : rating.Rate < 0
                    ? 0
                    : rating.Rate;
        }
    }
}
