using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Business
{
    public class OrderService :IOrderService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Announcement> _announcementRepository;

        public OrderService(IRepository<Product> productRepository, IRepository<Announcement> announcementRepository)
        {
            _productRepository = productRepository;
            _announcementRepository = announcementRepository;
        }

        public void CalculateValues(Order order)
        {
            CompleteIncludes(order);
            order.ValueWithouDiscount = order.OrderProducts.Select(op => op.Product.Price).Sum();
            order.DiscountPorcentage = _announcementRepository.Get(order.AnnouncementId).DiscountPorcentage;
        }

        private void CompleteIncludes(Order order)
        {
            order.OrderProducts.ToList().ForEach(x => x.Product = _productRepository.Get(x.ProductId));
        }
    }
}
