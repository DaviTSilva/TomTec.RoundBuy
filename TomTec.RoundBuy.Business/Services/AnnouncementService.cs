﻿using System;
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
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderProductsRepository _orderProductsRepository;

        public string[] Includes { get {
                return new string[]{
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.Images)}",
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}",
                    $"{nameof(Announcement.ProductPacks)}.{nameof(ProductPack.Product)}.{nameof(Product.OrderProducts)}",
                    $"{nameof(Announcement.Ratings)}",
                    $"{nameof(Announcement.Ratings)}.{nameof(Rating.AuthorUser)}",
                    $"{nameof(Announcement.Comments)}",
                    $"{nameof(Announcement.Comments)}.{nameof(Comment.AuthorUser)}",
                };
            } } 

        public AnnouncementService(IRepository<Announcement> announcementRepository, IRepository<Order> orderRepository, IOrderProductsRepository orderProductsRepository)
        {
            _announcementRepository = announcementRepository;
            _orderRepository = orderRepository;
            _orderProductsRepository = orderProductsRepository;
        }

        public IEnumerable<Announcement> GetAnnouncements(string shearchText = "", double? minimalPrice = 0, double? maximumPrice = double.MaxValue)
        {
            minimalPrice = minimalPrice == null ? 0 : minimalPrice;
            maximumPrice = maximumPrice == null ? double.MaxValue : maximumPrice;
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
                
            return _announcementRepository.Get(query, Includes);
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

        public int GetSoldProductsCounter(int announcementId)
        {
            var announcement = _announcementRepository.Get(announcementId, Includes);
            int counter = 0;

            foreach (var productPack in announcement.ProductPacks)
            {
                try
                {
                    _orderProductsRepository.GetByProductId(productPack.ProductId).ToList().ForEach(x => counter++);
                }
                catch (KeyNotFoundException)
                {
                    continue;
                }
               
            }

            return counter;
        }
    }
}
