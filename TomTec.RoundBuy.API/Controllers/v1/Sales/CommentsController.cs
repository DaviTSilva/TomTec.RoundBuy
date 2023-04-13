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

namespace TomTec.RoundBuy.API.Controllers.v1
{
    [Route("v1/comments")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class CommentsController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRepository<Comment> _commentRepository;
        public CommentsController(IAuthService authService, IRepository<Comment> commentRepository)
        {
            _authService = authService;
            _commentRepository = commentRepository;
        }

        [HttpPost("")]
        public IActionResult CreateComment([FromBody]CommentDto commentDto)
        {
            int authorUserId = _authService.GetCurrentUser(User).Id;
            var comment = _commentRepository.Create(commentDto.ToModel(authorUserId));

            return Created(ResponseMessage.Success, comment);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            return Ok(new 
            {
                message = ResponseMessage.Success,
                value = _commentRepository.Get(id, c => c.AuthorUser)
            });
        }

        [HttpGet("announcement/{announcementId}")]
        public IActionResult GetCommentByAnnouncement(int announcementId)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _commentRepository.Get(c => c.AnnouncementId == announcementId, c => c.AuthorUser)
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromBody]UpdateCommentDto commentDto, int id)
        {
            var comment = _commentRepository.Get(id);
            comment.CommentText = commentDto.CommentText;

            if (comment.AuthorUserId != _authService.GetCurrentUser(User).Id)
                throw new UnauthorizedAccessException();

            _commentRepository.Update(comment);

            return Ok(new {
                message = ResponseMessage.Success
            });
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _commentRepository.Delete(id);

            return Ok(new
            {
                message = ResponseMessage.Success
            });
        }
    }
}
