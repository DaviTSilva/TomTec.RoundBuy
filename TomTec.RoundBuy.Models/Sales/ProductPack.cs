using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class ProductPack : BaseEntity
    {
        public Announcement Announcement { get; set; }
        public int AnnouncementId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
