using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class OrderDto
    {
        public int AnnouncementId { get; set; }
        public IEnumerable<int> ProductsIds { get; set; }
        public double ValueWithoutDiscount { get; set; }
        public double DiscountPorcentage { get; set; }
        public bool IsParcelated { get; set; }
        public int ParcelsQuantity { get; set; }

        public Order ToModel(int buyerUserId)
        {
            var order = new Order() 
            {
                BuyerUserId = buyerUserId,
                AnnouncementId = this.AnnouncementId,
                ValueWithouDiscount = this.ValueWithoutDiscount,
                DiscountPorcentage = this.DiscountPorcentage,
                IsParcelated = this.IsParcelated,
                ParcelsQuantity = this.ParcelsQuantity,
            };
            order.OrderProducts = this.ProductsIds.Select(id => new OrderProducts() { ProductId = id, Order = order}).ToList();

            return order;
        }
    }
}
