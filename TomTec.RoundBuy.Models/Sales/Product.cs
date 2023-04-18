using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Product : BaseEntity
    {
        [Column(TypeName = "varchar(120)")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Model { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Color { get; set; }
        public double Price { get; set; }
        public bool IsSold { get; set; }
        public ICollection<TechnicalInfo> TechnicalInfos { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
