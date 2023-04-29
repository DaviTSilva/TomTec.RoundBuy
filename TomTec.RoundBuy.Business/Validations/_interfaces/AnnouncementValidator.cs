using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;
using TomTec.RoundBuy.Lib.Utils;
using System.Linq;

namespace TomTec.RoundBuy.Business
{
    static public class AnnouncementValidator
    {
        static public void Validate(this Announcement announcement)
        {
            if (string.IsNullOrEmpty(announcement.Title))
                throw new InvalidOperationException("Announcement title can not be empty!");

            if (announcement.Title.ContainsProfanity()
                || announcement.Description.ContainsProfanity()
                || announcement.ProductPacks.Any(x => x.Product.Title.ContainsProfanity() 
                    || x.Product.Model.ContainsProfanity() 
                    || x.Product.TechnicalInfos.Any(y => y.Title.ContainsProfanity() 
                        || y.InfoSeparetedBySemicolon.ContainsProfanity()
                        )
                    )
                )
                throw new InvalidOperationException("Profane words are not allowed!");

        }
    }
}
