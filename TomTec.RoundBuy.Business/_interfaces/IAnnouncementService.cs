using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public interface IAnnouncementService
    {
        public IEnumerable<Announcement> GetAnnouncements(string shearchText = "", double? minimalPrice = 0, double? maximumPrice = double.MaxValue);
        public int GetSoldProductsCounter(int announcementId);
    }
}
