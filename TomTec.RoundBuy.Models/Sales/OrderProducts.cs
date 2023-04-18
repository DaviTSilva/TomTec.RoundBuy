using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class OrderProducts
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
