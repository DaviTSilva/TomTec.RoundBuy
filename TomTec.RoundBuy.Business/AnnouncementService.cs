using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IRepository<Announcement> _announcementRepository;

        public AnnouncementService(IRepository<Announcement> announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public IEnumerable<Announcement> GetAnnouncements(string shearchText = "", double? minimalPrice = 0, double? maximumPrice = double.MaxValue)
        {
            Func<Announcement, bool> query;

            if (!string.IsNullOrEmpty(shearchText))
            {
                query = x => x.IsActive = true 
                    && (
                        x.Title.Contains(shearchText) 
                        || x.Description.Contains(shearchText) 
                        || IsAnyTrue(x.ProductPacks.Select(p => p.Product.Title.Contains(shearchText)))
                        || IsAnyTrue(x.ProductPacks.Select(p => p.Product.Color.Contains(shearchText)))
                        || IsAnyTrue(x.ProductPacks.Select(p => p.Product.Model.Contains(shearchText)))
                    )
                    && IsAnyTrue(x.ProductPacks.Select(p => p.Product.Price > minimalPrice))
                    && IsAnyTrue(x.ProductPacks.Select(p => p.Product.Price < maximumPrice));
            }
            else
            {
                query = x => x.IsActive = true
                    && IsAnyTrue(x.ProductPacks.Select(p => p.Product.Price > minimalPrice))
                    && IsAnyTrue(x.ProductPacks.Select(p => p.Product.Price < maximumPrice));
            }
                
            return _announcementRepository.Get(query);
        }

        private bool IsAnyTrue(IEnumerable<bool> values)
        {
            foreach (var item in values)
            {
                if (item)
                    return true;
                continue;
            }
            return false;
        }
    }
}
