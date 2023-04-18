using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Order : BaseEntity
    {
        public User BuyerUser { get; set; }
        public int BuyerUserId { get; set; }
        public Announcement Announcement { get; set; }
        public int AnnouncementId { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
        public double ValueWithouDiscount { get; set; }
        public double DiscountPorcentage { get; set; }
        public double DiscountValue { get { return ValueWithouDiscount * DiscountPorcentage; } }
        public double FinalValue { get { return ValueWithouDiscount - DiscountValue; } }
        public bool IsParcelated { get; set; }
        public int ParcelsQuantity { get; set; }
    }
}
