using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Announcement : BaseEntity
    {
        public User AdvertiserUser { get; set; }
        public int AdvertiserUserId { get; set; }

        [Column(TypeName = "varchar(120)")]
        [Required]
        public string Title { get; set; }
        public Address AlternativeAddress { get; set; }
        public int AlternativeAddressId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public ICollection<ProductPack> ProductPacks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public int MinimalSaleQuantity { get; set; } 
        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsAllSold { get; set; }

        [NotMapped]
        public int TotalProducts { get { return this.ProductPacks.Select(x => x.Quantity).Sum(); } }

        [NotMapped]
        public double AverageRate { get { return this.Ratings.Select(x => x.Rate).Average(); } }
    }
}
