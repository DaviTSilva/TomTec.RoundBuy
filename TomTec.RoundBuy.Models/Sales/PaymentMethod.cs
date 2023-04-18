using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class PaymentMethod : BaseEntity
    {
        [Column(TypeName = "varchar(120)")]
        [Required]
        public string Name { get; set; }
    }
}
