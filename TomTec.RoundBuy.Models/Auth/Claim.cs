using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Claim : BaseEntity
    {
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string Name { get; set; }

        public ICollection<UsersClaims> UsersClaims { get; set; }
    }
}
