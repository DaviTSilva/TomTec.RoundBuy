using System;
using System.Collections.Generic;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Payment : BaseEntity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }
        public bool IsPaymentConfirmed { get; set; }
        public DateTime Expire { get; set; }
        public bool IsActive { get; set; }
    }
}
