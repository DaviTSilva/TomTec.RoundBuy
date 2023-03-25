using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TomTec.RoundBuy.Models
{
    public class Announcement : BaseEntity
    {
        public User AdvertiserUser { get; set; }
        public int AdvertiserUserId { get; set; }
        public string Title { get; set; }
        public Address AlternativeAddress { get; set; }
        public int AlternativeAddressId { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }

        [NotMapped]
        public double AverageRate { get { return this.Ratings.Select(x => x.Rate).Average(); } }
    }
}
