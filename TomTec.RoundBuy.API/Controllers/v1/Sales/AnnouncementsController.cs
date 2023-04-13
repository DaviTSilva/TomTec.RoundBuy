using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IAnnouncementService _announcementService;
        private readonly IAuthService _authService;

        private readonly string[] includes = new string[]{
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.Images)}",
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}",
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.OrderProducts)}",
                    $"{nameof(Announcement.Ratings)}",
                    $"{nameof(Announcement.Comments)}",
                };

        public AnnouncementsController(IRepository<Announcement> announcementRepository, IAnnouncementService announcementService, IAuthService authService)
        {
            _announcementRepository = announcementRepository;
            _announcementService = announcementService;
            _authService = authService;
        }

        [HttpPost("")]
        public IActionResult CreateAnnouncement([FromBody] AnnouncementDto announcementDto)
        {
            var announcement = _announcementRepository.Create(announcementDto.ToModel(_authService.GetCurrentUser(User).Id));

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
                value = _announcementRepository.Get(id, includes),
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnnouncemnt(int id, [FromBody] AnnouncementDto announcementDto)
        {
            if(_announcementRepository.Get(id).AdvertiserUserId != _authService.GetCurrentUser(User).Id)
                throw new UnauthorizedAccessException();

            var announcement = announcementDto.ToModel(_authService.GetCurrentUser(User).Id);
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
            if (announcement.AdvertiserUserId != _authService.GetCurrentUser(User).Id)
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
            if (announcement.AdvertiserUserId != _authService.GetCurrentUser(User).Id)
                throw new UnauthorizedAccessException();

            announcement.IsActive = true;
            _announcementRepository.Update(announcement);

            return Ok(new
            {
                message = ResponseMessage.Success,
            });
        }

        [HttpGet("{id}/sold-products-counter")]
        public IActionResult GetSoldProductsCounter(int id)
        {
            return Ok(new 
            {
                message = ResponseMessage.Success,
                value = _announcementService.GetSoldProductsCounter(id),
            });
        }
    }
}
