using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class OrderProducts
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public OrderProducts()
        {

        }

        public OrderProducts(int orderId, int productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
