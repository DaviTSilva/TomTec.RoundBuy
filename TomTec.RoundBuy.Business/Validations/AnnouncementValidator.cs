using System;
using TomTec.RoundBuy.Models;
using TomTec.RoundBuy.Lib.Utils;
using System.Linq;

namespace TomTec.RoundBuy.Business
{
    static public class AnnouncementValidator
    {
        static public void Validate(this Announcement announcement)
        {
            if (announcement.MinimalSaleQuantity > announcement.TotalProducts)
                throw new InvalidOperationException($"[INVALID \"{nameof(Announcement)}\"! WRONG VALUE FOR FIELD \"{nameof(Announcement.MinimalSaleQuantity)}\"]: Announcement's minimal sales quantity can not be higher than the total number of products!");

            if (announcement.DiscountPorcentage > 1 || announcement.DiscountPorcentage < 0)
                throw new InvalidOperationException($"[INVALID \"{nameof(Announcement)}\"! WRONG VALUE FOR FIELD \"{nameof(Announcement.DiscountPorcentage)}\"]: The discount porcentage can not be higher than 100% or lower than 0%");

            if (string.IsNullOrEmpty(announcement.Title))
                throw new InvalidOperationException($"[INVALID \"{nameof(Announcement)}\"! WRONG VALUE FOR FIELD \"{nameof(Announcement.Title)}\"]: Announcement title can not be empty!");

            if (announcement.Description.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Announcement)}\"! WRONG VALUE FOR FIELD \"{nameof(Announcement.Description)}\"]: The announcemnt description can not contain profane words!");

            if (announcement.Title.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Announcement)}\"! WRONG VALUE FOR FIELD \"{nameof(Announcement.Title)}\"]: The announcement title can not contain profane words!");

            announcement.ProductPacks.ToList().ForEach(x => x.Product.Validate());
        }
    }
}
