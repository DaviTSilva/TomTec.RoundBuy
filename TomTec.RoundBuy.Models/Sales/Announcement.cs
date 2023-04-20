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
        public int? AlternativeAddressId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public ICollection<ProductPack> ProductPacks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public int MinimalSaleQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public bool IsAllSold { get; set; } = false;
        public double DiscountPorcentage { get; set; }

        [NotMapped]
        public int? TotalProducts { get { return this.ProductPacks == null || this.ProductPacks.Count == 0? 0 : this.ProductPacks.Select(x => x.Quantity).Sum(); } }

        [NotMapped]
        public double? AverageRate { get { return this.Ratings == null || this.Ratings.Count == 0? 0 : this.Ratings.Select(x => x.Rate).Average(); } }
    }
}
