using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public class RatingService : IRatingService
    {
        readonly private IRepository<Order> _orderRepository;

        public RatingService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void CheckIfUserCanRate(int userId, int announcementId)
        {
            try
            {
                var orders = _orderRepository.Get(o => o.BuyerUserId == userId && o.AnnouncementId == announcementId);
            }
            catch (KeyNotFoundException)
            {
                throw new UnauthorizedAccessException("An user can only rate an announcemnet after buying one of its products");
            }
        }
    }
}
