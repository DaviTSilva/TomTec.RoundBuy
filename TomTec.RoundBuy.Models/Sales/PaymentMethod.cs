using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class PaymentMethod : BaseEntity
    {
        [Key]
        public override int Id { get; set; }

        [Column(TypeName = "varchar(120)")]
        [Required]
        public string Name { get; set; }
    }

    public enum PaymentMethodEnum 
    { 
        CreditCard = 1,
        Boleto = 2,
        Pix = 3,
    }
}
