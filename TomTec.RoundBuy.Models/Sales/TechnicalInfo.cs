using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class TechnicalInfo : BaseEntity
    {
        [Column(TypeName = "varchar(120)")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string InfoSeparetedBySemicolon { get; set; }
    }
}
