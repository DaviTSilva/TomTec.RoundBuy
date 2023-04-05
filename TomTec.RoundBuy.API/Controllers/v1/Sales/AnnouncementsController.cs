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
    [Route("v1/announcements")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class AnnouncementsController : Controller
    {
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IRepository<Announcement> announcementRepository, IRepository<User> userRepository, IAnnouncementService announcementService)
        {
            _announcementRepository = announcementRepository;
            _userRepository = userRepository;
            _announcementService = announcementService;
        }

        [HttpPost("")]
        public IActionResult CreateAnnouncement([FromBody] AnnouncementDto announcementDto)
        {
            var announcement = _announcementRepository.Create(announcementDto.ToModel(GetCurrentUserId()));

            return Created(ResponseMessage.Success, announcement);
        }

        [HttpGet("")]
        public IActionResult GetAnnoucements(string shearchText = "", double? minimalPrice = null, double? maximumPrice = null)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _announcementService.GetAnnouncements(shearchText, minimalPrice, maximumPrice),
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetAnnouncementById(int id)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _announcementRepository.Get(id),
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnnouncemnt(int id, [FromBody] AnnouncementDto announcementDto)
        {
            if(_announcementRepository.Get(id).AdvertiserUserId != GetCurrentUserId())
                throw new UnauthorizedAccessException();

            var announcement = announcementDto.ToModel(GetCurrentUserId());
            announcement.Id = id;
            _announcementRepository.Update(announcement);

            return Ok(new 
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnnouncemnt(int id)
        {
            var announcement = _announcementRepository.Get(id);
            if (announcement.AdvertiserUserId != GetCurrentUserId())
                throw new UnauthorizedAccessException();

            announcement.IsActive = false;
            _announcementRepository.Update(announcement);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpPost("reactivate/{id}")]
        public IActionResult ReactivateAnnouncemnt(int id)
        {
            var announcement = _announcementRepository.Get(id);
            if (announcement.AdvertiserUserId != GetCurrentUserId())
                throw new UnauthorizedAccessException();

            announcement.IsActive = true;
            _announcementRepository.Update(announcement);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        private int GetCurrentUserId()
        {
            var userName = User.Identity.Name;
            var userId = _userRepository.Get(u => u.UserName.Equals(userName)).FirstOrDefault().Id;
            return userId;
        }
    }
}
